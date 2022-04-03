using Library.Context.Animal;
using Library.Models.Animal;
using System.Collections.Generic;

namespace Animal.Casting
{
    public class PetCast
    {
        private readonly ClassPet classPet;
        public PetCast()
        {
            classPet = new ClassPet();
        }

        public IEnumerable<Pet> List()
        {
            var listPet = new List<Pet>();
            var allPets = classPet.List();

            if (allPets != null)
            {
                foreach (var pets in allPets)
                {
                    var pet = new Pet()
                    {
                        Id = pets.Id,
                        Name = pets.Name,
                        Age = pets.Age,
                        Birthday = pets.Birthday,
                        Genre = pets.Genre,
                        Type = pets.Type,
                        PersonId = pets.PersonId,
                        Image = new Image()
                        {
                            Id = pets.Image.Id,
                            Tag = pets.Image.Tag,
                            Path = pets.Image.Path
                        },
                        Health = new Health()
                        {
                            Id = pets.Health.Id,
                            Status = pets.Health.Status
                        },
                        Service = new Service()
                        {
                            Id = pets.Service.Id,
                            Category = pets.Service.Category
                        }
                    };

                    listPet.Add(pet);
                }

                return listPet;
            }
            else
            {
                return null;
            }
        }
        public Pet Get(int? Id)
        {
            var pets = classPet.Get(Id);

            if (pets != null)
            {
                var pet = new Pet()
                {
                    Id = pets.Id,
                    Name = pets.Name,
                    Age = pets.Age,
                    Birthday = pets.Birthday,
                    Genre = pets.Genre,
                    Type = pets.Type,
                    PersonId = pets.PersonId,
                    Image = new Image()
                    {
                        Id = pets.Image.Id,
                        Tag = pets.Image.Tag,
                        Path = pets.Image.Path
                    },
                    Health = new Health()
                    {
                        Id = pets.Health.Id,
                        Status = pets.Health.Status
                    },
                    Service = new Service()
                    {
                        Id = pets.Service.Id,
                        Category = pets.Service.Category
                    }
                };
                return pet;
            }
            else
            {
                return null;
            }
        }
        public void Post(Pet pet)
        {
            var petLibrary = new PetLibrary()
            {
                Id = pet.Id,
                Name = pet.Name,
                Age = pet.Age,
                Birthday = pet.Birthday,
                Genre = pet.Genre,
                Type = pet.Type,
                PersonId = pet.PersonId,
                Image = new ImageLibrary()
                {
                    Id = pet.Image.Id,
                    Tag = pet.Image.Tag,
                    Path = pet.Image.Path
                },
                Health = new HealthLibrary()
                {
                    Id = pet.Health.Id,
                    Status = pet.Health.Status
                },
                Service = new ServiceLibrary()
                {
                    Id = pet.Service.Id,
                    Category = pet.Service.Category
                }
            };

            classPet.Post(petLibrary);
        }
        public void Put(Pet pet, int? Id)
        {
            var petLibrary = new PetLibrary()
            {
                Id = pet.Id,
                Name = pet.Name,
                Age = pet.Age,
                Birthday = pet.Birthday,
                Genre = pet.Genre,
                Type = pet.Type,
                PersonId = pet.PersonId,
                Image = new ImageLibrary()
                {
                    Id = pet.Image.Id,
                    Tag = pet.Image.Tag,
                    Path = pet.Image.Path
                },
                Health = new HealthLibrary()
                {
                    Id = pet.Health.Id,
                    Status = pet.Health.Status
                },
                Service = new ServiceLibrary()
                {
                    Id = pet.Service.Id,
                    Category = pet.Service.Category
                }
            };

            classPet.Put(petLibrary, Id);
        }
        public void Delete(int? Id)
        {
            classPet.Delete(Id);
        }
    }
}