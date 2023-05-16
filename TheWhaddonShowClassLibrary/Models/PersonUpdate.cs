﻿using MyClassLibrary.LocalServerMethods;
using System.Text.Json.Serialization;

namespace TheWhaddonShowClassLibrary.Models
{
    /// <summary>
    /// A person taking part in the show. (or previous participant if inActive)
    /// </summary>
    public class PersonUpdate : LocalServerIdentityUpdate
    {
        /// <summary>
        /// The First Name of the person.
        /// </summary>
        public string FirstName { get; set; } = string.Empty;
        /// <summary>
        /// The Last Name of the Person
        /// </summary>
        public string? LastName { get; set; }
        /// <summary>
        /// The email of the Person
        /// </summary>
        public string? Email { get; set; }
        /// <summary>
        /// A file path to the picture used within the app for the person.
        /// </summary>
        public string? PictureRef { get; set; }
        /// <summary>
        /// Identifies if the person is an actor. (available to be allocated to Parts)
        /// </summary>
        public  bool? IsActor { get; set; } = false;
        /// <summary>
        /// Identifies if the person is a singer. (available to be allocated to Singing Parts)
        /// </summary>
        public bool? IsSinger { get; set; } = false;
        /// <summary>
        /// Identifies if the person is a writer. (able to edit the Script)
        /// </summary>
        public bool? IsWriter { get; set; } = false;
        /// <summary>
        /// Identifies if the person is in the Band.
        /// </summary>
        public bool? IsBand { get; set; } = false;
        /// <summary>
        /// Identifies if the person is part of the technical team.
        /// </summary>
        public bool? IsTechnical { get; set; } = false;
        /// <summary>
        /// Identifies if the person is part of the technical team.
        /// </summary>
        public List<string> Tags { get; set; }

       
        public PersonUpdate(Guid id, string firstName, string? lastName = null, string? email = null, string? pictureRef = null, bool? isActor = null,
           bool? isSinger = null, bool? isWriter = null, bool? isBand = null, bool? isTechnical = null,List<string>? tags = null) : base(id)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PictureRef = pictureRef;
            IsActor = isActor ?? false;
            IsSinger = isSinger ?? false;
            IsWriter = isWriter ?? false;
            IsBand = isBand ?? false;
            IsTechnical = isTechnical ?? false;
            Tags = tags ?? new List<string> ();
        }

        [JsonConstructor]
        public PersonUpdate(Guid id, DateTime created, string createdBy, DateTime? updatedOnServer, bool isActive, string firstName, string? lastName = null, string? email = null, string? pictureRef = null, bool? isActor = null,
           bool? isSinger = null, bool? isWriter = null, bool? isBand = null, bool? isTechnical = null, List<string>? tags = null) : base(id)
        {
            Id = id;
            Created = created;
            CreatedBy = createdBy;
            UpdatedOnServer = updatedOnServer;
            IsActive = isActive;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PictureRef = pictureRef;
            IsActor = isActor ?? false;
            IsSinger = isSinger ?? false;
            IsWriter = isWriter ?? false;
            IsBand = isBand ?? false;
            IsTechnical = isTechnical ?? false;
            Tags = tags ?? new List<string>();
        }
    }
}
