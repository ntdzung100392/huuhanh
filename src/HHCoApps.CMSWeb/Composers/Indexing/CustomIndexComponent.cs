using Examine;
using HHCoApps.CMSWeb.Composers.Indexing.Creators;
using Umbraco.Core.Composing;

namespace HHCoApps.CMSWeb.Composers.Indexing
{
    public class CustomIndexComponent : IComponent
    {
        private readonly IExamineManager _examineManager;
        private readonly ContentIndexCreator _productIndexCreator;
        private readonly QuestionIndexCreator _questionIndexCreator;

        public CustomIndexComponent(IExamineManager examineManager, ContentIndexCreator productIndexCreator, QuestionIndexCreator questionIndexCreator)
        {
            _examineManager = examineManager;
            _productIndexCreator = productIndexCreator;
            _questionIndexCreator = questionIndexCreator;
        }

        public void Initialize()
        {
            foreach (var index in _productIndexCreator.Create())
            {
                _examineManager.AddIndex(index);
            }

            foreach (var index in _questionIndexCreator.Create())
            {
                _examineManager.AddIndex(index);
            }
        }

        public void Terminate()
        {
        }
    }
}