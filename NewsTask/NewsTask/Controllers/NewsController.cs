using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewsTask.Api.Extensions;
using NewsTask.Api.Models;
using NewsTask.Data.Repositories;
using NewsTask.Service;

namespace NewsTask.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NewsController : ControllerBase
    {
        ICollectionService _collectionService;
        ISourceService _sourceService;
        ICollectionRepository _collectionRepository;

        public NewsController(ICollectionService collectionService, ISourceService sourceService, ICollectionRepository collectionRepository)
        {
            _collectionService = collectionService;
            _sourceService = sourceService;
            _collectionRepository = collectionRepository;
        }

        //Creating new collection
        [HttpPost("collection/create/{title}")]
        public async Task CreateCollection(string title)
        {
            if (string.IsNullOrEmpty(title))
            {
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync("Title is empty.");
                return;
            }

            var collectionId = _collectionRepository.CreateCollection(title);

            if(collectionId == 0)
            {
                Response.StatusCode = StatusCodes.Status409Conflict;
                await Response.WriteAsync("Collection already exists with the same title.");
                return;
            }

            Ok(collectionId);
        }

        //Adding new sources into your collection
        [HttpPost("collection/addsource")]
        public async Task AddSourceToCollection(int sourceId, int collectionId)
        {
            if(sourceId <= 0 && collectionId <= 0)
            {
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync("Source id or collection id is less than zero.");
                return;
            }

            if(!_collectionRepository.AddSourceToCollection(sourceId, collectionId))
            {
                Response.StatusCode = StatusCodes.Status409Conflict;
                await Response.WriteAsync("This source already exists in this collection.");
                return;
            }

            Ok();
        }

        //All news from sources which was added to collection
        [HttpGet("source/{id}")]
        public async Task GetNewsForSource(int id)
        {
            if (id <= 0)
            {
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync("Source id can not be less than zero.");
                return;
            }

            Ok(_sourceService.GetNewsFromSource(id).ParseRSS());
        }

        //Getting news for collection
        [HttpGet("collection/getnews/{id}")]
        public async Task GetCollectionNews(int id)
        {
            if (id <= 0)
            {
                Response.StatusCode = StatusCodes.Status400BadRequest;
                await Response.WriteAsync("Collection id id can not be less than zero.");
                return;
            }

            Ok(_collectionService.GetCollectionNews(id).ParseRSSList());
        }

        //Getting all sources info "id+title"
        [HttpGet("sources/all")]
        public List<string> GetAllSources()
        {
            return _collectionRepository.GetAllSources();
        }
    }
}