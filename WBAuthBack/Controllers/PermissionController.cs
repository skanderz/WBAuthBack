﻿using System.Threading.Tasks;
using WBAuth.BLL.IManager;
using WBAuth.BO;
using Microsoft.AspNetCore.Mvc;

namespace WBAuthBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionController : ControllerBase
    {
        private readonly IPermissionManager _PermissionManager;
        public PermissionController(IPermissionManager PermissionManager)  { _PermissionManager = PermissionManager;  }



        //GET : api/Permission/List
        [HttpGet]
        [Route("ListMultiFonction/{IdApplication}/{IdRole}")]
        public async Task<IActionResult> ChargerAllMultiFonction(int IdApplication, int IdRole)
        {
            var permissions = await _PermissionManager.ChargerAllMultiFonction(IdApplication, IdRole);
            if (permissions == null)  return NoContent();
            return Ok(permissions);
        }


        //GET : api/Permission/List
        [HttpGet]
        [Route("ListFonctionUnique/{IdApplication}/{IdRole}")]
        public async Task<IActionResult> ChargerAllFonctionUnique(int IdApplication, int IdRole)
        {
            var permissions = await _PermissionManager.ChargerAllFonctionUnique(IdApplication, IdRole);
            if (permissions == null) return NoContent();
            return Ok(permissions);
        }



        //GET : api/Permission/idPermission
        [HttpGet]
        [Route("RechercheFonctionUnique/Get/{id}/{IdApplication}/{IdRole}")]
        public async Task<IActionResult> RechercheMultiFonction(int Id, int IdApplication, int IdRole)
        {
            var oPermission = await _PermissionManager.RechercheMultiFonctionById(Id, IdApplication, IdRole);
            if (oPermission == null)  return NoContent();
            return Ok(oPermission);
        }


        //GET : api/Permission/idPermission
        [HttpGet]
        [Route("RechercheMultiFonction/Get/{id}/{IdApplication}/{IdRole}")]
        public async Task<IActionResult> RechercheFonctionUnique(int Id, int IdApplication, int IdRole)
        {
            var oPermission = await _PermissionManager.RechercheFonctionUniqueById(Id, IdApplication, IdRole);
            if (oPermission == null) return NoContent();
            return Ok(oPermission);
        }


        //GET : api/Permission/idPermission
        [HttpGet]
        [Route("RechercheFonctionUnique/{rech}/{IdApplication}/{IdRole}")]
        public async Task<IActionResult> RechercheMultiFonction(string rech, int IdApplication, int IdRole)
        {
            var oPermission = await _PermissionManager.RechercheMultiFonction(rech, IdApplication, IdRole);
            if (oPermission == null) return NoContent();
            return Ok(oPermission);
        }


        //GET : api/Permission/idPermission
        [HttpGet]
        [Route("RechercheMultiFonction/{rech}/{IdApplication}/{IdRole}")]
        public async Task<IActionResult> RechercheFonctionUnique(string rech, int IdApplication, int IdRole)
        {
            var oPermission = await _PermissionManager.RechercheFonctionUnique(rech, IdApplication, IdRole);
            if (oPermission == null) return NoContent();
            return Ok(oPermission);
        }



        //POST : api/Permission/ajouter
        [HttpPost]
        [Route("ajouter")]
        public async Task<IActionResult> Ajouter([FromBody] Permission oPermission)
        {
            if (!ModelState.IsValid)  {  return BadRequest(ModelState);  }
            var id = await _PermissionManager.Ajouter(oPermission);
            if (id <= 0)   return BadRequest($"Une erreur est survenue lors de la création de la Permission {oPermission.Nom}.");
            return Ok(id);
        }


        //PUT : api/Permission/modifier
        [HttpPut]
        [Route("modifier")]
        public async Task<IActionResult> Modifier([FromBody] Permission oPermission ,string type)
        {
            if (!ModelState.IsValid)  {   return BadRequest(ModelState);  }
            if(oPermission.Fonction.Type == "unique"){ 
                var perm = await _PermissionManager.RechercheFonctionUniqueById(oPermission.Id, oPermission.Fonction.IdApplication, oPermission.IdRole);
                if (perm == null)  return NotFound("Cet Permission est introuvable'");
            }
            if (oPermission.Fonction.Type == "multi"){
                var perm = await _PermissionManager.RechercheMultiFonctionById(oPermission.Id, oPermission.Fonction.IdApplication, oPermission.IdRole);
                if (perm == null) return NotFound("Cet Permission est introuvable'");
            }

            var id = await _PermissionManager.Modifier(oPermission ,type);
            if (id <= 0)  return BadRequest($"Une erreur est survenue lors de la mise à jour de le Permission: {oPermission.Nom}.");     
            return Ok(id);
        }


        //PUT : api/Permission/modifierAcces
        [HttpPut]
        [Route("modifierAcces")]
        public async Task<IActionResult> ModifierAcces(int Id, int IdApplication, int IdRole, int i)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            var perm = await _PermissionManager.RechercheFonctionUniqueById(Id, IdApplication, IdRole);
            var perm2 = await _PermissionManager.RechercheMultiFonctionById(Id, IdApplication, IdRole);
            if (perm == null && perm2 == null) return NotFound("Cet Permission est introuvable'");

            var id = await _PermissionManager.ModifierAcces(Id, IdApplication, IdRole, i);
            if (id <= 0) return BadRequest($"Une erreur est survenue lors de la mise à jour de le Permission avec l'id : {Id}.");
            return Ok(id);
        }



        //DELETE : api/Permission/supprimer
        [HttpDelete]
        [Route("supprimer")]
        public async Task<IActionResult> Supprimer(int id)
        {
            if (id <= 0) {   return BadRequest("Permission introuvable");  }
            var isdeleted = await _PermissionManager.Supprimer(id);
            if (!isdeleted)  return BadRequest($"Une erreur est survenue lors de la suppression de Permission.");
            return Ok(isdeleted);
        }



    }
}

