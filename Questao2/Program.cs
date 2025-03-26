using Newtonsoft.Json;
using Questao2.Domain.Model;

public class Program
{
    private const string urlBase = @"https://jsonmock.hackerrank.com/api/football_matches";
    public static async Task Main()
    {
        string teamName = "Paris Saint-Germain";
        int year = 2013;
        int totalGoals = await getTotalScoredGoals(teamName, year);

        Console.WriteLine("Team "+ teamName +" scored "+ totalGoals.ToString() + " goals in "+ year);

        teamName = "Chelsea";
        year = 2014;
        totalGoals = await getTotalScoredGoals(teamName, year);

        Console.WriteLine("Team " + teamName + " scored " + totalGoals.ToString() + " goals in " + year);

        // Output expected:
        // Team Paris Saint - Germain scored 109 goals in 2013
        // Team Chelsea scored 92 goals in 2014
    }

    public static async Task<int> getTotalScoredGoals(string team, int year)
    {
        int totalGoals = 0;

        totalGoals += await GetGoalsFromApi(team, year, true);

        totalGoals += await GetGoalsFromApi(team, year, false);

        return totalGoals;
    }

    public static async Task<int> GetGoalsFromApi(string team, int year, bool isTeam1)
    {
        int totalGoals = 0;
        int page = 1;
        int totalPages = 1;

        do
        {
            string url = $"{urlBase}?year={year}&{(isTeam1 ? "team1" : "team2")}={team}&page={page}";

            ApiResponseModel response = await GetCompetitionDataAsync(url);

            if (response == null)
                break;

            foreach (var match in response.Data)
            {
                if (isTeam1)
                {
                    if (int.TryParse(match.Team1Goals, out int team1Goals))
                        totalGoals += team1Goals;
                }
                else
                {
                    if (int.TryParse(match.Team2Goals, out int team2Goals))
                        totalGoals += team2Goals;
                }
            }

            totalPages = response.TotalPages;
            page++;

        } while (page <= totalPages);

        return totalGoals;
    }

    private static async Task<ApiResponseModel> GetCompetitionDataAsync(string url)
    {
        using HttpClient client = new();

        HttpResponseMessage response = await client.GetAsync(url);
        response.EnsureSuccessStatusCode(); 

        string jsonResponse = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<ApiResponseModel>(jsonResponse);
    }
}