using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApplication.Context;
using WebApplication.Models;

namespace WebApplication.DAL.Cadastros
{
    public class PetDAL
    {
        private EFContext context = new EFContext();
        public IQueryable<Pet> ObterPetsClassificadosPorId()
        {
            return context.Pets.OrderBy(b => b.PetId);
        }

        public Pet ObterPetPorId(long id)
        {
            return context.Pets.Where(c => c.PetId == id).First();
        }
        public void GravarPet(Pet pet)
        {
            if (pet.PetId == 0)
            {
                context.Pets.Add(pet);
            }
            else
            {
                context.Entry(pet).State = EntityState.Modified;
            }
            context.SaveChanges();
        }
        public Pet EliminarPetPorId(long id)
        {
            Pet pet = ObterPetPorId(id);
            context.Pets.Remove(pet);
            context.SaveChanges();
            return pet;
        }
    }
}