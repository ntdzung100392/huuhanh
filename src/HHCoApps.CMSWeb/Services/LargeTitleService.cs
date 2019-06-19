using System;
using AutoMapper;
using HHCoApps.CMSWeb.Models;
using Umbraco.Web.PublishedModels;

namespace HHCoApps.CMSWeb.Services
{
    public class LargeTileService : ILargeTileService
    {
        private readonly IMapper _mapper;
        private string _defaultBackgroundColor = "#595854";
        private string _defaultFontColor = "#ffffff";

        public LargeTileService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public LargeTileViewModel GetViewModel(LargeTile largeTile)
        {
            var viewModel = _mapper.Map<LargeTileViewModel>(largeTile);
            viewModel.BackgroundColor = viewModel.BackgroundColor.Equals("#", StringComparison.OrdinalIgnoreCase) ? _defaultBackgroundColor : viewModel.BackgroundColor;
            viewModel.FontColor = viewModel.FontColor.Equals("#", StringComparison.OrdinalIgnoreCase) ? _defaultFontColor : viewModel.FontColor;
            return viewModel;
        }
    }

    public interface ILargeTileService
    {
        LargeTileViewModel GetViewModel(LargeTile largeTile);
    }
}