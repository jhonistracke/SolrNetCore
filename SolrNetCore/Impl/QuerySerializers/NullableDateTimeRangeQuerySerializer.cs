﻿#region license

// Copyright (c) 2007-2010 Mauricio Scheffer
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//      http://www.apache.org/licenses/LICENSE-2.0
//  
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

#endregion

using System;
using System.Linq;

namespace SolrNetCore.Impl.QuerySerializers {
    public class NullableDateTimeRangeQuerySerializer : SingleTypeQuerySerializer<SolrQueryByRange<DateTime?>> {
        private readonly ISolrFieldSerializer fieldSerializer;

        public NullableDateTimeRangeQuerySerializer(ISolrFieldSerializer fieldSerializer) {
            this.fieldSerializer = fieldSerializer;
        }

        public string SerializeSingle(object o) {
            if (o == null)
                return "*";
            return fieldSerializer.Serialize(o).First().FieldValue;
        }

        public override string Serialize(SolrQueryByRange<DateTime?> q) {
            return RangeQuerySerializer.BuildRange(q.FieldName, SerializeSingle(q.From), SerializeSingle(q.To), q.InclusiveFrom, q.InclusiveTo);
        }
    }
}