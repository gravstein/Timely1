using Models.Entities;

namespace Models.DTO
{
    // DTO (Data Transfer Object) - промежуточный объект для передачи данных на фронт. Вместо того чтобы передавать модель и
    // вместе с ней все её поля (нагружая память, json и теряя в безопасности), мы передаём DTO только с теми данными которые 
    // нам конкретно нужны. Если бы я хотел передать все данные то я бы просто создал несколько DTO

    // конкретно в этом случае в DTO просто записаны все поля модели

    public class GuitarDTO
    {
        /// <summary>
        /// Id of the Guitar
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of Guitar Model
        /// </summary>
        public string ModelName { get; set; }

        /// <summary>
        /// Price of the Guitar
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Number of Guitar Pickups
        /// </summary>
        public int NumOfPickups { get; set; }

        /// <summary>
        /// Type of Guitars wood
        /// </summary>
        public string TypeOfWood { get; set; }

        /// <summary>
        /// Brand of the Guitar
        /// </summary>
        public Brand Brand { get; set; }
        /// <summary>
        /// Category of the Guitar
        /// </summary>
        public Category Category { get; set; }
    }
}
