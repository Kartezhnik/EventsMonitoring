using EventsMonitoring.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Data.Entity;

namespace EventsMonitoring 
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Events Monitoring API", Version = "v1" });
            });

            builder.Services.AddDbContext<Context>(options => 
                options.UseMySql("Server=localhost;Database=applicationdb;User=root;Password=12345;", 
                new MySqlServerVersion(new Version(8, 0, 34, 0))));

            WebApplication app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Events Monitoring API V1");
                c.RoutePrefix = string.Empty; 
            });

            app.UseDefaultFiles();
            app.UseStaticFiles();
            
            app.MapGet("/api/events", async (Context db) => await db.Events.ToListAsync());

            app.MapGet("/api/events/eventId/{id}", async (Guid id, Context db) =>
            {
                Event? @event = await db.Events.FirstOrDefaultAsync(e => e.Id == id);

                if (@event == null) return Results.NotFound(new {message = "Событие не найдено"});

                return Results.Json(@event);
            });

            app.MapGet("/api/events/eventName/{name}", async (string name, Context db) =>
            {
                List<Event> events = await db.Events.Where(e => e.Name == name).ToListAsync();

                if (events.Count == 0) return Results.NotFound(new { message = "Событие не найдено" });

                return Results.Json(events);
            });

            app.MapGet("/api/events/dateOfEvent/{dateOfEvent}", async (DateTime dateOfEvent, Context db) => 
            {
                List<Event> events = await db.Events.Where(e => e.DateOfEvent == dateOfEvent).ToListAsync();

                if (events.Count == 0) return Results.NotFound(new { message = "Событие не найдено" });

                return Results.Json(events);
            });

            app.MapGet("/api/events/placeOfEvent/{placeOfEvent}", async (string placeOfEvent, Context db) =>
            {
                List<Event> events = await db.Events.Where(e => e.PlaceOfEvent == placeOfEvent).ToListAsync();

                if (events.Count == 0) return Results.NotFound(new { message = "Событие не найдено" });

                return Results.Json(events);
            });

            app.MapGet("/api/events/eventType/{type}", async (string type, Context db) =>
            {
                Event? @event = await db.Events.FirstOrDefaultAsync(e => e.Type == type);
                await db.Events.ToListAsync();
            });

            app.MapDelete("/api/events/eventDelete/{id}", async (Guid id, Context db) =>
            {
                Event? @event = await db.Events.FirstOrDefaultAsync(e => e.Id == id);

                if (@event == null) return Results.NotFound(new { message = "Событие не найдено" });

                db.Events.Remove(@event);
                db.SaveChanges();
                return Results.Json(@event);
            });

            app.MapPost("/api/events/eventAdd", async ([FromBody] Event @event, Context db) =>
            {
                await db.Events.AddAsync(@event);
                await db.SaveChangesAsync();
                return @event;
            });

            app.MapPut("/api/events/eventChange", async (Guid id, [FromBody] Event eventData, Context db) =>
            {
                Event? @event = await db.Events.FirstOrDefaultAsync(e => e.Id == id);

                if (@event == null) return Results.NotFound(new { message = "Событие не найдено" });

                @event.Name = eventData.Name;
                @event.Description = eventData.Description;
                @event.Type = eventData.Type;
                @event.DateOfEvent = eventData.DateOfEvent;
                @event.PlaceOfEvent = eventData.PlaceOfEvent;

                await db.SaveChangesAsync();
                return Results.Json(@event);
            });

            app.MapGet("/api/participants/participantId/{id}", async (Guid id, Context db) =>
            {
                Participant? participant = await db.Participants.FirstOrDefaultAsync(p => p.Id == id);

                if(participant == null) return Results.NotFound(new {message = "Участник не найден"});

                return Results.Json(participant);
            });

            app.MapPost("/api/event/{eventId}/participants/participantAddToEvent", async (int eventId, 
                [FromBody]Participant participant, Context db) =>
            {
                Event? @event = await db.Events.FindAsync(eventId);

                if (@event == null) return Results.NotFound(new { message = "Событие не найдено" });

                participant.EventInfoKey = eventId;
                await db.Participants.AddAsync(participant);
                await db.SaveChangesAsync();

                return Results.Json(participant);
            });

            app.MapGet("/api/event/{eventId}/participants", async (int eventId, 
                [FromBody] Participant participant, Context db) =>
            {
                Event? @event = await db.Events.FindAsync(eventId);

                if (@event == null) return Results.NotFound(new { message = "Событие не найдено" });

                participant.EventInfoKey = eventId;
                await db.Participants.ToListAsync();
                
                return Results.Json(participant);
            });

            app.MapGet("/api/participants/removeFromEvent/{participantId}/{eventId}", async (Guid id, int eventId, Context db) =>
            {
                Participant? participant = await db.Participants.FirstOrDefaultAsync(p => p.Id == id);

                if (participant == null) return Results.NotFound(new { message = "Участник не найден" });

                Event? @event = await db.Events.FindAsync(eventId);

                if (@event == null) return Results.NotFound(new { message = "Событие не найдено" });

                if(participant.EventInfoKey != eventId) return Results.BadRequest(new { message = "Участник не принадлежит к событию" });
                db.Participants.Remove(participant);
                await db.SaveChangesAsync();

                return Results.NoContent();
            });

            app.MapPost("/api/events/{eventId}/upload-image", async (int eventId, IFormFile imageFile, Context db) =>
            {
                Event? @event = await db.Events.FindAsync(eventId);
                if (@event == null) return Results.NotFound(new { message = "Событие не найдено" });

                if (imageFile == null || imageFile.Length == 0) return Results.NotFound(new { message = "Изображение не загружено" });
                var filePath = Path.Combine("uploads", imageFile.FileName); 
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }

                @event.ImageUrl = $"/uploads/{imageFile.FileName}"; 
                await db.SaveChangesAsync();

                return Results.Ok(@event);
            });

            app.Run();
        }
    }
}
