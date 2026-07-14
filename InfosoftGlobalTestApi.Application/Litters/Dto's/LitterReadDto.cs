using System;
using System.Collections.Generic;
using System.Text;

namespace InfosoftGlobalTestApi.Application.Litters.Dto_s
{
    public class LitterReadDto
    {
        public string Id { get; set; }
        public Guid BreederId { get; set; }
        public string Status { get; set; } 
        public DateTime CreatedAt { get; set; }


        public LitterReadDto()
        {
        }
    }
}
