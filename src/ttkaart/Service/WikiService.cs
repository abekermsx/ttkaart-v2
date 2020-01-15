using ttkaart.ViewModels;
using ttkaart.DAL;

namespace ttkaart.Service
{
    public class WikiService
    {

        public WikiListViewModel GetWiki()
        {
            var wiki = new WikiListViewModel();
            
            wiki.playerWiki = Database.Instance.wikiRepository.GetPlayersWithWiki();
            wiki.clubWiki = Database.Instance.wikiRepository.GetClubsWithWiki();

            return wiki;
        }
    }
}