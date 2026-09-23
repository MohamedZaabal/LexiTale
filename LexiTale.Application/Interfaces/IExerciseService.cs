using LexiTale.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexiTale.Application.Interfaces
{
    public interface IExerciseService
    {
        Task<StoryResultResponse> SubmitAsync(
           SubmitStoryRequest request);
    }
}

