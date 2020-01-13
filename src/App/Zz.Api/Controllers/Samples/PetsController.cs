using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Zz.Http.Core.Controllers;

namespace Zz.Api.Controllers.Samples
{
    #region snippet_Inherit
    [Produces(MediaTypeNames.Application.Json)]
    [Route("[controller]")]
    public class PetsController : BaseController
    #endregion
    {
        private static readonly List<Pet> _petsInMemoryStore = new List<Pet>();

        public PetsController()
        {
            if (_petsInMemoryStore.Count == 0)
            {
                _petsInMemoryStore.Add(
                    new Pet
                    {
                        Breed = "Collie",
                        Id = 1,
                        Name = "Fido",
                        PetType = PetType.Dog
                    });
            }
        }

        [HttpGet]
        public ActionResult<List<Pet>> GetAll() => _petsInMemoryStore;

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Pet> GetById(int id)
        {
            var pet = _petsInMemoryStore.FirstOrDefault(p => p.Id == id);

            #region snippet_ProblemDetailsStatusCode
            if (pet == null)
            {
                return NotFound();
            }
            #endregion

            return pet;
        }

        #region snippet_400And201
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Pet> Create(Pet pet)
        {
            pet.Id = _petsInMemoryStore.Any() ?
                     _petsInMemoryStore.Max(p => p.Id) + 1 : 1;
            _petsInMemoryStore.Add(pet);

            return CreatedAtAction(nameof(GetById), new { id = pet.Id }, pet);
        }
        #endregion

        public class Pet
        {
            public int Id { get; set; }

            public string Breed { get; set; }

            public string Name { get; set; }

            [Required]
            public PetType PetType { get; set; }
        }

        public enum PetType
        {
            Dog = 0,
            Cat = 1
        }
    }
}
