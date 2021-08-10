using System;
using System.Collections.Generic;
using System.Text;
using _0_Framework.Application;
using ShopManagement.Application.Contracts.Slide;
using ShopManagement.Domain.SlideAgg;

namespace ShopManagement.Application.Slide
{
    public class SlideApplication : ISlideApplication
    {
        private readonly IFileUploader _fileUploader;
        private readonly ISlideRepository _slideRepository;

        public SlideApplication(ISlideRepository slideRepository, IFileUploader fileUploader)
        {
            _slideRepository = slideRepository;
            _fileUploader = fileUploader;
        }

        public OperationResult Create(CreateSlide command)
        {
            var operationResult = new OperationResult();

            var fileName = _fileUploader.Upload(command.Picture, "slides");

            var slide = new Domain.SlideAgg.Slide(fileName, command.PictureAlt, command.PictureTitle,
                command.Heading,command.Title ,command.Text, command.BtnText,command.Link);

            _slideRepository.Create(slide);
            _slideRepository.SaveChanges();
            return operationResult.Succeeded();

        }

        public OperationResult Edit(EditSlide command)
        {
            var operationResult = new OperationResult();
            var slide = _slideRepository.Get(command.Id);
            if (slide == null)
                return operationResult.Failed(ApplicationMessages.NotFound);

            var fileName = _fileUploader.Upload(command.Picture, "slides");

            slide.Edit(fileName, command.PictureAlt, command.PictureTitle,
                command.Heading, command.Title, command.Text, command.BtnText, command.Link);

            _slideRepository.SaveChanges();
            return operationResult.Succeeded();
        }

        public OperationResult Remove(long id)
        {
            var operationResult = new OperationResult();
            var slide = _slideRepository.Get(id);
            if (slide == null)
                return operationResult.Failed(ApplicationMessages.NotFound);

            slide.Remove();

            _slideRepository.SaveChanges();
            return operationResult.Succeeded();
        }

        public OperationResult Restore(long id)
        {
            var operationResult = new OperationResult();
            var slide = _slideRepository.Get(id);
            if (slide == null)
                return operationResult.Failed(ApplicationMessages.NotFound);

            slide.Restore();

            _slideRepository.SaveChanges();
            return operationResult.Succeeded();
        }

        public EditSlide GetDetails(long id)
        {
            return _slideRepository.GetDetails(id);
        }

        public List<SlideViewModel> GetList()
        {
            return _slideRepository.GetList();
        }
    }
}
