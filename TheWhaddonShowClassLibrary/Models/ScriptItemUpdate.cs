﻿
using MyClassLibrary.LocalServerMethods.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TheWhaddonShowClassLibrary.Models
{
    /// <summary>
    /// Base Class for all ScriptItems
    /// </summary>
    public class ScriptItemUpdate : LocalServerModelUpdate//, IHasParentId<Guid>
    {
        /// <summary>
        /// The Id of the parent script Item of this script item.
        /// </summary>
        public Guid? ParentId { get; set; }
        /// <summary>
        /// The order no in which this appears amongst other items with the same Parent
        /// </summary>
        public int OrderNo { get; set; }

        /// <summary>
        /// The type of script item this is i.e Show, Act, Scene, Line, Paragraph or Span 
        /// </summary>
        /// [Required]
        [RegularExpression("Show|Act|Scene|Synopsis|Dialogue|Action|Lighting|Sound|Staging"
                           , ErrorMessage = "An invalid scriptItemType was given.\n" +
                            "Valid Types:  - Show|Act|Scene|Synopsis|Dialogue|Action|Lighting|Sound|Staging")]
        public string Type { get; set; } = string.Empty;


        /// <summary>
        /// Title of the scene, piece of dialogue, action description etc...
        /// </summary>
        public string Text { get; set; } = string.Empty;

        /// <summary>
        /// List of Parts associated with the script item. If null defaults to 
        /// </summary>
        public List<Guid>? PartIds { get; }

        /// <summary>
        /// List of string that can be used as tags.
        /// </summary>
        public List<string>? Tags { get; set; }
     
        public ScriptItemUpdate()
        {

        }


        public ScriptItemUpdate(Guid id, Guid? parentId, int orderNo, string type, List<Part>? parts = null, List<string>? tags = null) : base(id)
        {
            ParentId = parentId ?? Guid.Empty;
            OrderNo = orderNo;
            PartIds = parts?.Select(x=>x.Id).ToList();
            Type = type;
            Tags = tags;
            
        }


        [JsonConstructor]   
        public ScriptItemUpdate(Guid id, DateTime created, string createdBy, DateTime? updatedOnServer,bool isConflicted,bool isSample, bool isActive, Guid? parentId, int orderNo, string type,string text, List<Guid>? partIds, List<string>? tags = null) : base(id)
        {
            Id= id;
            Created = created;
            CreatedBy = createdBy;
            UpdatedOnServer = updatedOnServer;
            IsConflicted = isConflicted;
            IsSample = isSample;
            IsActive = isActive;
            ParentId = parentId ;
            OrderNo = orderNo;
            Type = type;
            Text = text;
            PartIds = partIds ;
            Tags = tags;  

        }

     

        public List<Part> Parts() {
            throw new NotImplementedException();
         }

    }
}
