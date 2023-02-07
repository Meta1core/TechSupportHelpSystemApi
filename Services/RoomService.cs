using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using TechSupportHelpSystem.DAL;
using TechSupportHelpSystem.Log;
using TechSupportHelpSystem.Models;
using TechSupportHelpSystem.Models.DAO;
using TechSupportHelpSystem.Repositories;

namespace TechSupportHelpSystem.Services
{
    public class RoomService : IRoomService
    {
        IClientService ClientService;

        public RoomService(MasterContext masterContext)
        {
            ClientService = new ClientService(masterContext);
        }

        public HttpResponseMessage CreateRoom(int id_Client, Room room, Claim currentUserClaims)
        {
            try
            {
                Client client = ClientService.GetClient(id_Client);
                DbContextOptions clientOptions = ClientService.GetClientOptions(client);
                using (ApplicationContext db = new ApplicationContext(clientOptions))
                {
                    db.Schdlr_Resource.Add(room);
                    db.SaveChanges();
                    NLogger.Logger.Info("|Client № {0}|User {1} added room  with ID_Room - {2}| Title - {3} ", id_Client, currentUserClaims.Value, room.ID_Resource, room.Title);
                }
                return new HttpResponseMessage(System.Net.HttpStatusCode.Created);
            }
            catch (Exception e)
            {
                HttpResponseMessage httpResponse = new HttpResponseMessage();
                httpResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
                httpResponse.ReasonPhrase = e.InnerException.Message;
                NLogger.Logger.Error(e);
                return httpResponse;
            }
        }

        public HttpResponseMessage DeleteRoom(int id_Client, int id_Room, Claim currentUserClaims)
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
                    NLogger.Logger.Info("|Client № {0}|User {1} deleted room  with ID_Room - {2}| Title - {3} ", id_Client, currentUserClaims.Value, room.ID_Resource, room.Title);
                }
                return new HttpResponseMessage(System.Net.HttpStatusCode.NoContent);
            }
            catch (Exception e)
            {
                HttpResponseMessage httpResponse = new HttpResponseMessage();
                httpResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
                httpResponse.ReasonPhrase = e.InnerException.Message;
                NLogger.Logger.Error(e);
                return httpResponse;
            }
        }

        public HttpResponseMessage UpdateRoom(int id_Client, Room room, Claim currentUserClaims)
        {
            try
            {
                Client client = ClientService.GetClient(id_Client);
                DbContextOptions clientOptions = ClientService.GetClientOptions(client);
                using (ApplicationContext db = new ApplicationContext(clientOptions))
                {
                    UpdateRoomFields(room, db);
                    db.SaveChanges();
                    NLogger.Logger.Info("|Client № {0}|User {1} changed room  with ID_Room - {2}| Title - {3} ", id_Client, currentUserClaims.Value, room.ID_Resource, room.Title);
                }
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                HttpResponseMessage httpResponse = new HttpResponseMessage();
                httpResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
                httpResponse.ReasonPhrase = e.InnerException.Message;
                NLogger.Logger.Error(e);
                return httpResponse;
            }
        }

        public Room GetRoom(int id_Client, int id_Room)
        {
            try
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
            catch (Exception e)
            {
                NLogger.Logger.Error(e);
                return null;
            }
        }

        public List<Room> GetRooms(int id_Client)
        {
            try
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
            catch (Exception e)
            {
                NLogger.Logger.Error(e);
                return null;
            }
        }

        public HttpResponseMessage EditRoomProcedures(int id_Client, int id_Room, int id_Modality, Claim currentUserClaims)
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
                    NLogger.Logger.Info("|Client № {0}|User {1} changed room procedures| ID_Room - {2} ", id_Client, currentUserClaims.Value, id_Room);
                }
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                HttpResponseMessage httpResponse = new HttpResponseMessage();
                httpResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
                httpResponse.ReasonPhrase = e.InnerException.Message;
                NLogger.Logger.Error(e);
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

        private void UpdateRoomFields(Room room, ApplicationContext db)
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
        }
    }
}
