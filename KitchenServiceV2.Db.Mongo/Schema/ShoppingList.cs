﻿using System.Collections.Generic;
using MongoDB.Bson;

namespace KitchenServiceV2.Db.Mongo.Schema
{
    public class ShoppingList : IDocument
    {
        public string Name { get; set; }
        public bool IsDone { get; set; }
        public long CreatedOnUnixSeconds { get; set; }
        public List<ShoppingListItem> Items { get; set; }
        public List<ShoppingListItem> OptionalItems { get; set; }
        public ObjectId Id { get; set; }
        public string UserToken { get; set; }
    }
}
