using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Runtime.Serialization;
using Rescuer.Core.Entities;

namespace Rescuer.Entites.Mongo
{
    public abstract class BaseEntity : IEntity
    {
        protected BaseEntity()
        {
            _id = ObjectId.GenerateNewId();
        }

        [BsonId]
        [DataMember]
        private ObjectId _id;

        [DataMember]
        public string ID
        {
            get => _id.ToString();
            set => _id = string.IsNullOrEmpty(value) ? ObjectId.GenerateNewId() : new ObjectId(value);
        }

        [DataMember]
        public bool IsActive { get; set; }
        [DataMember]
        public bool IsDeleted { get; set; }
        [DataMember]
        public DateTime CreatedDate { get; set; }
        [DataMember]
        public DateTime? UpdatedDate { get; set; }
        [DataMember]
        public string CreatedUserID { get; set; }
        [DataMember]
        public string UpdatedUserID { get; set; }
    }
}


