using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Library.ViewModels;
using Library.Data.Repositories;

namespace Library.Services
{
    public class ItemService
    {
        private BookRepository _bookRepository;

        public ItemService()
        {
            _bookRepository = new BookRepository();
        }

        public EditBookViewModel GetBookForEditById(int bookId)
        {
            var result = _bookRepository.GetBook(bookId);
            var model = new EditBookViewModel();
            model.Id = result.Id;
            model.Name = result.Name;
            model.Author = result.Author;
            model.Publisher = result.Publisher;
            model.YearOfPublishing = result.YearOfPublishing;
            //model.IncludeInHomePage = result.IncludeInHomePage;
            return model;
        }


        public bool SaveBook(AddBookViewModel model)
        {
            _bookRepository.SaveBook(model);
            var result = true;
            return result;
        }

        public bool EditBook(EditBookViewModel model)
        {
            var tags = _tagRepository.GetAllTags();
            var saveModel = new EditBookDbViewModel();
            saveModel.Title = model.Title;
            saveModel.Context = model.Context;
            saveModel.Tags = JsonSerializeHelper.Serialize(model.Tags);
            saveModel.IncludeInHomePage = model.IncludeInHomePage;
            var result = _bookRepository.EditBook(saveModel);
            
            return result;
        }

        //public void IncludePostInHome(string postId, bool include)
        //{
        //    _bookRepository.IncludePostInHome(postId, include);
        //}
        
        public void DestroyBook(int id)
        {
            _bookRepository.DestroyBook(id);
        }
    }
}