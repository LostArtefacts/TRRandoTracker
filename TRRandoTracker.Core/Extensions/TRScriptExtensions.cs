using System.Text;
using TRGE.Core;

namespace TRRandoTracker.Core.Extensions;

public static class TRScriptExtensions
{
    public static string GetDecodedName(this TR2ScriptedLevel level)
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < level.Name.Length; i++)
        {
            char c = level.Name[i];
            char d;
            if (i == level.Name.Length - 1)
            {
                d = c;
            }
            else
            {
                d = level.Name[i + 1];
            }

            switch (c)
            {
                case '$':
                    switch (d)
                    {
                        case 'A':
                            sb.Append('À');
                            break;
                        case 'E':
                            sb.Append('È');
                            break;
                        case 'I':
                            sb.Append('Ì');
                            break;
                        case 'O':
                            sb.Append('Ò');
                            break;
                        case 'U':
                            sb.Append('Ù');
                            break;
                        case 'a':
                            sb.Append('à');
                            break;
                        case 'e':
                            sb.Append('è');
                            break;
                        case 'i':
                            sb.Append('ì');
                            break;
                        case 'o':
                            sb.Append('ò');
                            break;
                        case 'u':
                            sb.Append('ù');
                            break;
                    }
                    i++;
                    break;

                case ')':
                    switch (d)
                    {
                        case 'A':
                            sb.Append('Á');
                            break;
                        case 'C':
                            sb.Append('Ć');
                            break;
                        case 'E':
                            sb.Append('É');
                            break;
                        case 'I':
                            sb.Append('Í');
                            break;
                        case 'N':
                            sb.Append('Ń');
                            break;
                        case 'S':
                            sb.Append('Ś');
                            break;
                        case 'O':
                            sb.Append('Ó');
                            break;
                        case 'U':
                            sb.Append('Ú');
                            break;
                        case 'Y':
                            sb.Append('Ý');
                            break;
                        case 'Z':
                            sb.Append('Ź');
                            break;

                        case 'a':
                            sb.Append('á');
                            break;
                        case 'c':
                            sb.Append('ć');
                            break;
                        case 'e':
                            sb.Append('é');
                            break;
                        case 'i':
                            sb.Append('í');
                            break;
                        case 'n':
                            sb.Append('ń');
                            break;
                        case 's':
                            sb.Append('ś');
                            break;
                        case 'o':
                            sb.Append('ó');
                            break;
                        case 'u':
                            sb.Append('ú');
                            break;
                        case 'y':
                            sb.Append('ý');
                            break;
                        case 'z':
                            sb.Append('ź');
                            break;
                    }
                    i++;
                    break;

                case '(':
                    switch (d)
                    {
                        case 'A':
                            sb.Append('Â');
                            break;
                        case 'E':
                            sb.Append('Ê');
                            break;
                        case 'I':
                            sb.Append('Î');
                            break;
                        case 'O':
                            sb.Append('Ô');
                            break;
                        case 'U':
                            sb.Append('Û');
                            break;
                        case 'a':
                            sb.Append('â');
                            break;
                        case 'e':
                            sb.Append('ê');
                            break;
                        case 'i':
                            sb.Append('î');
                            break;
                        case 'o':
                            sb.Append('ô');
                            break;
                        case 'u':
                            sb.Append('û');
                            break;
                    }
                    i++;
                    break;

                case '~':
                    switch (d)
                    {
                        case 'A':
                            sb.Append('Ä');
                            break;
                        case 'E':
                            sb.Append('Ë');
                            break;
                        case 'I':
                            sb.Append('Ï');
                            break;
                        case 'O':
                            sb.Append('Ö');
                            break;
                        case 'U':
                            sb.Append('Ü');
                            break;
                        case 'Y':
                            sb.Append('Ÿ');
                            break;
                        case 'a':
                            sb.Append('ä');
                            break;
                        case 'e':
                            sb.Append('ë');
                            break;
                        case 'i':
                            sb.Append('ï');
                            break;
                        case 'o':
                            sb.Append('ö');
                            break;
                        case 'u':
                            sb.Append('ü');
                            break;
                        case 'y':
                            sb.Append('ÿ');
                            break;
                    }
                    i++;
                    break;

                default:
                    sb.Append(ConvertChar(c));
                    break;
            }
        }
        return sb.ToString();
    }

    private static char ConvertChar(char c)
    {
        switch (c)
        {
            case '<':
                return '(';
            case '>':
                return ')';
            case '[':
                return '↑';
            case ']':
                return '↓';
            case '+':
                return '&';
            case '=':
                return 'ß';
            default:
                return c;
        }
    }
}