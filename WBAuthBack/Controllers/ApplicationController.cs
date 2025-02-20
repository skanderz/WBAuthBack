﻿using System.Threading.Tasks;
using WBAuth.BLL.IManager;
using WBAuth.BO;
using Microsoft.AspNetCore.Mvc;

namespace WBAuth.Back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        private readonly IApplicationManager _ApplicationManager;
        public ApplicationController(IApplicationManager ApplicationManager)  { _ApplicationManager = ApplicationManager;  }


        //GET : api/Application/List
        [HttpGet]
        [Route("List")]
        public async Task<IActionResult> ChargerAll()
        {
            var oApplication = await _ApplicationManager.ChargerAll();
            if (oApplication == null)  return NoContent();
            return Ok(oApplication);
        }


        //GET : api/Application/idApplication
        [HttpGet]
        [Route("Get/{id}")]
        public async Task<IActionResult> GetAplicationById(int id)
        {
            var oApplication = await _ApplicationManager.RechercheById(id);
            if (oApplication == null) return NoContent();
            return Ok(oApplication);
        }


        //GET : api/Application/idApplication
        [HttpGet]
        [Route("{rech}")]
        public async Task<IActionResult> GetApplicationByName(string rech)
        {
            var oApplication = await _ApplicationManager.Recherche(rech);
            if (oApplication == null)  return NoContent();
            return Ok(oApplication);
        }


        //POST : api/Application/ajouter
        [HttpPost]
        [Route("ajouter")]
        public async Task<IActionResult> Ajouter([FromBody] Application oApplication)
        {
            if (!ModelState.IsValid)  {  return BadRequest(ModelState);  }
            var id = await _ApplicationManager.Ajouter(oApplication);
            if (id <= 0)   return BadRequest($"Une erreur est survenue lors de la création de l'application {oApplication.Nom}.");
            return Ok(id);
        }


        //PUT : api/Application/modifier
        [HttpPut]
        [Route("modifier")]
        public async Task<IActionResult> Modifier([FromBody] Application oApplication)
        {
            if (!ModelState.IsValid)  {   return BadRequest(ModelState);  }
            var app = await _ApplicationManager.RechercheById(oApplication.Id);
            if (app == null)  return NotFound("Cet application est introuvable'");
            var id = await _ApplicationManager.Modifier(oApplication);
            if (id <= 0)  return BadRequest($"Une erreur est survenue lors de la mise à jour de l'application {oApplication.Nom}.");     
            return Ok(id);
        }


        //DELETE : api/Application/supprimer
        [HttpDelete]
        [Route("supprimer")]
        public async Task<IActionResult> Supprimer(int id)
        {
            if (id <= 0) {   return BadRequest("Application introuvable");  }
            var Application = await _ApplicationManager.RechercheById(id);
            if (Application == null)  return NotFound("Application est introuvable'");
            var isdeleted = await _ApplicationManager.Supprimer(id);
            if (!isdeleted)  return BadRequest($"Une erreur est survenue lors de la suppression de Application.");
            return Ok(isdeleted);
        }






    }
}

