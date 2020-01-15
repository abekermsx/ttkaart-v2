using ttkaart.ViewModels;
using ttkaart.DAL;

namespace ttkaart.Service
{
    public class SearchService
    {

        private bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }


        private bool IsValidBondsnummer(string str)
        {
            var rest = 1000;

            for (var i = 0; i < 6; i++)
                rest -= ((7 - i) * (str[i] - 48));
            
            return (rest % 10 == (str[6]-48));
        }


        public SearchViewModel Search(string text)
        {
            var result = new SearchViewModel();

            result.SearchTerm = text;

            result.clubs = Database.Instance.clubRepository.GetClubsByText(text);

            result.players = new PlayerOverviewModel();

            result.IsBondsnummer = IsDigitsOnly(text);

            if (result.IsBondsnummer)
            {
                text = text.PadLeft(7, '0');

                result.IsValidBondsnummer = IsValidBondsnummer(text);
                
                var player = Database.Instance.playerRepository.GetPlayerByNumber(text);

                if (player != null)
                {
                    var p = new PlayerListViewModel();

                    p.playerName = player.playerName;
                    p.playerNr = player.playerNumber;

                    result.players.players.Add(p);
                }
            }
            else
            {
                result.players.players = Database.Instance.playerRepository.GetPlayersByText(text);
            }

            return result;
        }

    }
}