﻿using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace TRRandoTracker.Updates;

public class UpdateChecker
{
    private static UpdateChecker _instance;

    public static UpdateChecker Instance
    {
        get => _instance ??= new ();
    }

    private const string _updateUrl = "https://api.github.com/repos/lahm86/TRRandoTracker/releases/latest";

    private readonly TimeSpan _initialDelay, _periodicDelay;
    private readonly CancellationTokenSource _cancelSource;
    private readonly CancellationToken _cancelToken;

    public event EventHandler<UpdateEventArgs> UpdateAvailable;

    public Update LatestUpdate { get; private set; }

    private UpdateChecker()
    {
        //required for Win7 - https://stackoverflow.com/questions/2859790/the-request-was-aborted-could-not-create-ssl-tls-secure-channel
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

        _initialDelay = new TimeSpan(0, 0, 5);
        _periodicDelay = new TimeSpan(0, 30, 0);

        _cancelSource = new CancellationTokenSource();
        _cancelToken = _cancelSource.Token;

        Application.Current.Exit += Application_Exit;

        Run();
    }

    private void Application_Exit(object sender, ExitEventArgs e)
    {
        _cancelSource.Cancel();
    }

    private async void Run()
    {
        await Task.Delay(_initialDelay, _cancelToken);
        do
        {
            if (!_cancelToken.IsCancellationRequested)
            {
                try
                {
                    CheckForUpdates();
                }
                catch { }

                await Task.Delay(_periodicDelay, _cancelToken);
            }
        }
        while (!_cancelToken.IsCancellationRequested);
    }

    public bool CheckForUpdates()
    {
        HttpClient client = new();
        client.DefaultRequestHeaders.UserAgent.Add(new("TRRandoTracker", "1.0"));
        client.DefaultRequestHeaders.CacheControl = new()
        {
            NoCache = true
        };

        HttpResponseMessage response = client.Send(new(HttpMethod.Get, _updateUrl));
        response.EnsureSuccessStatusCode();

        using Stream receiveStream = response.Content.ReadAsStream();
        using StreamReader reader = new(receiveStream);

        JObject releaseInfo = JObject.Parse(reader.ReadToEnd());
        if (!releaseInfo.ContainsKey("tag_name"))
        {
            throw new IOException("Invalid response from GitHub - missing tag_name field.");
        }

        string currentVersion = ((App)Application.Current).TaggedVersion;
        string latestVersion = releaseInfo["tag_name"].ToString();
        if (string.Compare(latestVersion, currentVersion, true) == 0)
        {
            return false;
        }

        LatestUpdate = new()
        {
            CurrentVersion = currentVersion.ToUpper(),
            NewVersion = latestVersion.ToUpper(),
            ReleaseDate = DateTime.Parse(releaseInfo["published_at"].ToString()),
            UpdateBody = Regex.Replace(releaseInfo["body"].ToString(), "<.*?>", string.Empty),
            UpdateURL = releaseInfo["html_url"].ToString()
        };

        UpdateAvailable?.Invoke(this, new UpdateEventArgs(LatestUpdate));

        return true;

        //HttpWebRequest req = WebRequest.CreateHttp(_updateUrl);
        //req.UserAgent = "TRRandoTracker";
        //req.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);

        //using (WebResponse response = req.GetResponse())
        //using (Stream receiveStream = response.GetResponseStream())
        //using (StreamReader reader = new StreamReader(receiveStream))
        //{
        //    Dictionary<string, object> releaseInfo = JsonConvert.DeserializeObject<Dictionary<string, object>>(reader.ReadToEnd());
        //    string currentVersion = ((App)Application.Current).TaggedVersion;
        //    if (!releaseInfo.ContainsKey("tag_name"))
        //    {
        //        throw new IOException("Invalid response from GitHub - missing tag_name field.");
        //    }

        //    string latestVersion = releaseInfo["tag_name"].ToString();
        //    if (latestVersion.Equals(currentVersion))
        //    {
        //        return false;
        //    }

        //    LatestUpdate = new Update
        //    {
        //        CurrentVersion = currentVersion,
        //        NewVersion = latestVersion,
        //        ReleaseDate = DateTime.Parse(releaseInfo["published_at"].ToString()),
        //        UpdateBody = releaseInfo["body"].ToString(),
        //        UpdateURL = releaseInfo["html_url"].ToString()
        //    };

        //    UpdateAvailable?.Invoke(this, new UpdateEventArgs(LatestUpdate));

        //    return true;
        //}
    }
}