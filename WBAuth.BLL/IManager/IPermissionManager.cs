﻿using WBAuth.BO;


namespace WBAuth.BLL.IManager
{
    public interface IPermissionManager
    {
        Task<IEnumerable<Permission>> ChargerAllMultiFonction(int IdApplication, int IdRole);
        Task<IEnumerable<Permission>> ChargerAllFonctionUnique(int IdApplication, int IdRole);
        Task<Permission> RechercheMultiFonctionById(int Id);
        Task<Permission> RechercheFonctionUniqueById(int Id);
        Task<int> Ajouter(Permission oPermission);
        Task<int> Modifier(Permission oPermission);
        Task<int> ModifierAcces(int Id, int i);
        Task<bool> Supprimer(int Id);
    }



}


