using System.Collections.Generic;

namespace HHCoApps.CMSWeb.Models
{
    public class QuestionsViewModel
    {
        public IEnumerable<TopicModel> Topics { get; set; }
        public string NoResultsFoundMessage { get; set; }
    }
}