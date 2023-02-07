
using AutoMapper;
using CURD_Movies.Models;
using CURD_Movies.Service;
using CURD_Movies.ViewModel;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;


namespace CURD_Movies.Controllers
{
    public class MoviesController : Controller
    {

        private void saveImage(IFormFile file)
        {
            string uploadpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Images", file.FileName);
            FileStream fileStream = new FileStream(uploadpath, FileMode.Create);
            file.CopyToAsync(fileStream);
        }
        //private readonly IbaseServices<Movies> services;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IToastNotification _toastNotification;
        private readonly IMapper _mapper;
        

        public MoviesController(IUnitOfWork unitOfWork , IToastNotification toastNotification , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _toastNotification = toastNotification;
            _mapper = mapper;
                 
        }
        public async Task<IActionResult> Index()
        {
            return View(await _unitOfWork.Movies.getList());
        }

        public async Task<IActionResult> Create()
        {
            var ViewModel = new ViewModelMovies
            {
                Genres = await _unitOfWork.Genres.getList()
            };
            
            return View(ViewModel);
        }
        [HttpPost]
        
        public async Task<IActionResult> Create(ViewModelMovies model )
        {
            #region
            //string fileName = Path.GetFileName(formFile.FileName);
            //string uploadpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Images", fileName);
            //FileStream stream = new FileStream(uploadpath, FileMode.Create);
            //formFile.CopyToAsync(stream);
            //ViewBag.Message = "File uploaded successfully.";
            //ViewBag.ImageURL = "wwwroot\\Images\\\\" + fileName;

            ///////////////////////////////////////
            //string uploadpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Images",file1.FileName);
            //FileStream fileStream = new FileStream(uploadpath, FileMode.Create); 
            //file1.CopyToAsync(fileStream);
            #endregion


            if (!ModelState.IsValid)
            {
                model.Genres = await _unitOfWork.Genres.getList();
                return View(model);
            }
            model.Genres = await _unitOfWork.Genres.getList();
            var file = Request.Form.Files;
            model.Poster = file.FirstOrDefault().FileName;
             
            if (!file.Any())
            {
                ModelState.AddModelError("Poster", "Please Select Your Poster.....!");
                return View(model);
            }
            
            var file1 = Request.Form.Files.FirstOrDefault();
            //For Get Name of image and combine it in path Folder then save it 
            saveImage(file1);

            var Movie = _mapper.Map<Movies>(model);
            #region convert to mapper
            //var Movie = new Movies
            //{
            //    Name = model.Name,
            //    genreid = model.genre_Id,
            //    Image_address = file1.FileName,
            //    Description = model.Description,
            //    Rate = model.Rate
            //};
            #endregion

            await _unitOfWork.Movies.add(Movie); 
           Ok(model);
            _toastNotification.AddSuccessToastMessage("Movie added Successfuly ^_^");


            
           return RedirectToAction("Index");
        }
        
        public async Task<IActionResult> View(int? id)
        {
            if (!ModelState.IsValid)
            {
                return  BadRequest();
            }
            var movie = await _unitOfWork.Movies.getIndex(x => x.id == id, new[] { "Genre" } );
            if (movie == null)
                return NotFound();
            return View(movie);

        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var model = await _unitOfWork.Movies.get(x => x.id == id);
            if (model == null)
                return BadRequest();
            

            var movie = _mapper.Map<ViewModelMovies>(model);
            movie.Genres = await _unitOfWork.Genres.getList();
            #region Convert to Mapper
            //var movie = new ViewModelMovies
            //{
            //    id = model.id,
            //    Name = model.Name,
            //    genre_Id = model.genreid,
            //    Poster = model.Image_address,
            //    Description = model.Description,
            //    Rate = model.Rate,
            //    Genres = await _unitOfWork.Genres.getList()
            //};
            #endregion
            return View("Create",movie );

        }
        [HttpPost]
        public async Task<IActionResult> Edit(ViewModelMovies model )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (model == null)
                return BadRequest();

            var file = Request.Form.Files.FirstOrDefault();
            var movie = await  _unitOfWork.Movies.get(x => x.id == model.id);
            model.Poster = movie.Image_address;
            if(movie == null)
                return NotFound();
            if(file != null)
            {
                saveImage(file);
                //movie.Image_address = file.FileName;
                model.Poster = file.FileName;
                
            }
            #region convert to Mapper           
            //movie.Name = model.Name;
            //movie.genreid = model.genre_Id;           
            //movie.Description = model.Description;           
            //movie.Rate = model.Rate;
            //movie.genreid = model.genre_Id;
            #endregion
            movie = _mapper.Map(model , movie);
            await _unitOfWork.Movies.update(movie);
            _toastNotification.AddSuccessToastMessage("Movie Updated Successfuly ^_^");
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int? id )
        {
            if (id == null)
                return BadRequest();

            var model = await _unitOfWork.Movies.get(x => x.id == id);
            if (model == null)
                return NotFound();

            _unitOfWork.Movies.remove(model);

            _toastNotification.AddSuccessToastMessage("Movie Deleted Successfuly ^_^");

            return Ok(); 

        }

        





    }
}
