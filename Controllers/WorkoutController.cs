﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using workoutService.Entities;
using workoutService.Services;

namespace workoutService.Controllers
{
    [Route("[controller]")]
    public class WorkoutController : Controller
    {
        private readonly WorkoutService _workoutService;

        public WorkoutController(WorkoutService workoutService)
        {
            _workoutService = workoutService;
        }

        // GET: api/Workout
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<WorkoutModel>>> GetWorkouts()
        {
            return Ok(await _workoutService.GetWorkoutsAsync());
        }

        // GET: api/Workout/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<WorkoutModel>> GetWorkout(int id)
        {
            var workout = await _workoutService.GetWorkoutByIdAsync(id);

            if (workout == null)
            {
                return NotFound();
            }

            return Ok(workout);
        }

        // POST: api/Workout
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<WorkoutModel>> PostWorkout([FromBody] WorkoutModel workout)
        {
            var createdWorkout = await _workoutService.CreateWorkoutAsync(workout);

            // Returns a CreatedAtAction to produce a Status201Created response
            return CreatedAtAction(nameof(GetWorkout), new { id = createdWorkout.Id }, createdWorkout);
        }

        // PUT: api/Workout/5
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutWorkout(int id, [FromBody] WorkoutModel workout)
        {
            if (id != workout.Id)
            {
                return BadRequest();
            }

            try
            {
                await _workoutService.UpdateWorkoutAsync(workout);
            }
            catch (Exception)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/Workout/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteWorkout(int id)
        {
            var workout = await _workoutService.DeleteWorkoutAsync(id);

            if (workout == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}

