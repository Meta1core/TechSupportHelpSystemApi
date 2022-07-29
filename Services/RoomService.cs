using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using TechSupportHelpSystem.DAL;
using TechSupportHelpSystem.Models;

namespace TechSupportHelpSystem.Services
{
    public class RoomService : IRoomService
    {
        IClientService ClientService = new ClientService();
        public HttpResponseMessage CreateRoom(int id_Client, Room room)
        {
            try
            {
                Client client = ClientService.GetClient(id_Client);
                DbContextOptions clientOptions = ClientService.GetClientOptions(client);
                using (ApplicationContext db = new ApplicationContext(clientOptions))
                {
                    db.Schdlr_Resource.Add(room);
                    db.SaveChanges();
                }
                return new HttpResponseMessage(System.Net.HttpStatusCode.Created);
            }
            catch (Exception e)
            {
                HttpResponseMessage httpResponse = new HttpResponseMessage();
                httpResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
                httpResponse.ReasonPhrase = e.InnerException.Message;
                return httpResponse;
            }
        }

        public HttpResponseMessage DeleteRoom(int id_Client, int id_Room)
        {
            try
            {
                Client client = ClientService.GetClient(id_Client);
                DbContextOptions clientOptions = ClientService.GetClientOptions(client);
                using (ApplicationContext db = new ApplicationContext(clientOptions))
                {
                    Room room = db.Schdlr_Resource.Where(r => r.ID_Resource == id_Room).FirstOrDefault();
                    db.Schdlr_Resource.Remove(room);
                    db.SaveChanges();
                }
                return new HttpResponseMessage(System.Net.HttpStatusCode.NoContent);
            }
            catch (Exception e)
            {
                HttpResponseMessage httpResponse = new HttpResponseMessage();
                httpResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
                httpResponse.ReasonPhrase = e.InnerException.Message;
                return httpResponse;
            }
        }

        public HttpResponseMessage EditRoomProcedures(int id_Client, int id_Room, int id_Modality)
        {
            try
            {
                Client client = ClientService.GetClient(id_Client);
                DbContextOptions clientOptions = ClientService.GetClientOptions(client);
                using (ApplicationContext db = new ApplicationContext(clientOptions))
                {
                    List<ProcedureRef> procedures = db.ProcedureRef.Where(p => p.ID_Modality == id_Modality).ToList();
                    var query = db.ProcedureRef.Where(p => p.ID_Modality == id_Modality).Include(x => id_Modality);
                }
                return new HttpResponseMessage(System.Net.HttpStatusCode.NoContent);
            }
            catch (Exception e)
            {
                HttpResponseMessage httpResponse = new HttpResponseMessage();
                httpResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
                httpResponse.ReasonPhrase = e.InnerException.Message;
                return httpResponse;
            }
        }

        public Room GetRoom(int id_Client, int id_Room)
        {
            Room room;
            Client client = ClientService.GetClient(id_Client);
            DbContextOptions clientOptions = ClientService.GetClientOptions(client);
            using (ApplicationContext db = new ApplicationContext(clientOptions))
            {
                room = db.Schdlr_Resource.Where(r => r.ID_Resource == id_Room).FirstOrDefault();
            }
            return room;
        }
        public List<Room> GetRooms(int id_Client)
        {
            List<Room> rooms;
            Client client = ClientService.GetClient(id_Client);
            DbContextOptions clientOptions = ClientService.GetClientOptions(client);
            using (ApplicationContext db = new ApplicationContext(clientOptions))
            {
                rooms = db.Schdlr_Resource.ToList();
            }
            return rooms;
        }

        public HttpResponseMessage UpdateRoom(int id_Client, Room room)
        {
            try
            {
                Client client = ClientService.GetClient(id_Client);
                DbContextOptions clientOptions = ClientService.GetClientOptions(client);
                using (ApplicationContext db = new ApplicationContext(clientOptions))
                {
                    Room roomFromDatabase = db.Schdlr_Resource.Where(r => r.ID_Resource == room.ID_Resource).FirstOrDefault();
                    roomFromDatabase.Title = room.Title;
                    roomFromDatabase.ID_Clinic = room.ID_Clinic;
                    roomFromDatabase.OpenTime = room.OpenTime;
                    roomFromDatabase.Color = room.Color;
                    roomFromDatabase.DefaultAppointmentDuration = room.DefaultAppointmentDuration;
                    roomFromDatabase.SlotsTime = room.SlotsTime;
                    roomFromDatabase.Sequence = room.Sequence;
                    roomFromDatabase.SlotsTimeOff = room.SlotsTimeOff;
                    roomFromDatabase.StepValue = room.StepValue;
                    db.SaveChanges();
                }
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                HttpResponseMessage httpResponse = new HttpResponseMessage();
                httpResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
                httpResponse.ReasonPhrase = e.InnerException.Message;
                return httpResponse;
            }
        }
    }
}
