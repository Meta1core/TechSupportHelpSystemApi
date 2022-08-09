using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using TechSupportHelpSystem.DAL;
using TechSupportHelpSystem.Log;
using TechSupportHelpSystem.Models;
using TechSupportHelpSystem.Models.DAO;

namespace TechSupportHelpSystem.Services
{
    public class RoomService : IRoomService
    {
        IClientService ClientService = new ClientService();
        public HttpResponseMessage CreateRoom(int id_Client, Room room, string username)
        {
            try
            {
                Client client = ClientService.GetClient(id_Client);
                DbContextOptions clientOptions = ClientService.GetClientOptions(client);
                using (ApplicationContext db = new ApplicationContext(clientOptions))
                {
                    db.Schdlr_Resource.Add(room);
                    db.SaveChanges();
                    NLogger.Logger.Info("|Client № {0}|User {1} added room  with ID_Room - {2}| Title - {3} ", id_Client, username, room.ID_Resource, room.Title);
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

        public HttpResponseMessage DeleteRoom(int id_Client, int id_Room, string username)
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
                    NLogger.Logger.Info("|Client № {0}|User {1} deleted room  with ID_Room - {2}| Title - {3} ", id_Client, username, room.ID_Resource, room.Title);
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

        public HttpResponseMessage EditRoomProcedures(int id_Client, int id_Room, int id_Modality, string username)
        {
            try
            {
                List<ProcedureRef> procedures = new List<ProcedureRef>();
                Client client = ClientService.GetClient(id_Client);
                DbContextOptions clientOptions = ClientService.GetClientOptions(client);
                using (ApplicationContext db = new ApplicationContext(clientOptions))
                {
                    procedures = db.ProcedureRef.Where(p => p.ID_Modality == id_Modality && p.IsHidden == false).ToList();
                    ProcessServicesToRoom(procedures, id_Room, db);
                    NLogger.Logger.Info("|Client № {0}|User {1} changed room procedures with ID_Room - {2} ", id_Client, username, id_Room);
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

        public void ProcessServicesToRoom(List<ProcedureRef> procedureRefs, int id_Room, ApplicationContext db)
        {
            foreach (ProcedureRef pr in procedureRefs)
            {
                db.Schdlr_ResourceProcedureref.Add(new ProceduresToRoomDto() { ID_ProcedureRef = pr.ID_ProcedureRef, ID_Resource = id_Room });
            }
            db.SaveChanges();
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

        public HttpResponseMessage UpdateRoom(int id_Client, Room room, string username)
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
                    NLogger.Logger.Info("|Client № {0}|User {1} changed room  with ID_Room - {2}| Title - {3} ", id_Client, username, room.ID_Resource, room.Title);
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
