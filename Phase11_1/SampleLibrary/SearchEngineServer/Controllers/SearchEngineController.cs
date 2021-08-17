using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SampleLibrary;

namespace SearchEngineServer.Controllers
{
    [ApiController]
    [Route("[controller]/[Action]")]
    public class SearchEngineController : ControllerBase
    {
        private readonly Searcher _searcher;

        public SearchEngineController()
        {
            var searchContext = new SearchContext();
            _searcher = new Searcher(searchContext, new InvertedIndex(searchContext));
        }

        [HttpGet]
        public IEnumerable<Result> Search(string searchingExpression)
        {
            return Result.BuildResults(_searcher.Search(searchingExpression));
        }
    }
}