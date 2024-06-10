using System.Text;

public static class TextTransfer
{
    public static string GetContext(string before)
    {
        var sb = new StringBuilder();
        var split = before.Split(' ');

        foreach (var item in split)
        {
            switch (item)
            {
                default:
                    sb.Append(item);
                    break;
                case "{Player}":
                    sb.Append(GameManager.Data.Play.Name[0]);
                    break;
                case "{Debt}":
                    sb.Append(GameManager.Data.Play.Debt);
                    break;
                case "{Day}":
                    switch (GameManager.Data.Play.Day)
                    {
                        case DayFlow.Morning:
                            sb.Append("아침");
                            break;
                        case DayFlow.Noon:
                            sb.Append("점심");
                            break;
                        case DayFlow.AfterNoon:
                            sb.Append("오후");
                            break;
                        case DayFlow.Evening:
                            sb.Append("저녁");
                            break;
                        case DayFlow.Night:
                            sb.Append("밤");
                            break;
                    }
                    break;
            }
            sb.Append(" ");
        }

        return sb.ToString();
    }

    public static string[] GetDefaultName()
    {
        return new string[2] { "마보로시", "비비아" };
    }

    public static string GetBattleResultText(bool isWin)
    {
        if (isWin)
            return "승리!";
        return "패배...";
    }

    public static string GetDifficultyText(int difficulty)
    {
        return $"난이도 {difficulty}";
    }

    public static string[] GetCommunityText(CommunityData community)
    {
        return new string[] { 
            $"이름 : {community.Actor.Name[0]} {community.Actor.Name[1]}",
            $"호감도 : {community.Favor}",
            $"설명 : {community.Actor.Description}"
        };
    }

    public static string GetPointRemainText()
    {
        return "포인트가 남습니다!";
    }

    public static string GetPowerlessText()
    {
        return "근력이 부족합니다!";
    }

    public static string GetRemainPointText(int remain)
    {
        return $"잔여 : {remain}";
    }
}
