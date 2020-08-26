// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using System.IO;
using System.Linq;
using Dolittle.Machine.Specifications.MongoDB.given;
using Machine.Specifications;
using MongoDB.Driver;

namespace Dolittle.Machine.Specifications.MongoDB
{
    [Subject(typeof(a_mongo_db_instance))]
    public class when_importing_a_collection : a_mongo_db_instance
    {
        const string import_file_contents = "{\"_id\":{\"$binary\":\"J2yVQle8XUmsu/LaJM71lw==\",\"$type\":\"03\"},\"AString\":\"Old woman\",\"AnInt\":42,\"ADateTimeUTC\":{\"$date\":\"2019-02-12T13:27:38.680Z\"},\"ADateTimeLocal\":{\"$date\":\"2019-02-12T13:27:38.680Z\"},\"ADateTimeOffset\":[{\"$numberLong\":\"636855748586801310\"},0],\"AnArrayOfStrings\":[\"strange\",\"women\",\"lying\",\"in\",\"ponds\",\"distributing\",\"swords\",\"is\",\"no\",\"basis\",\"for\",\"a\",\"system\",\"of\",\"government\"]}\r\n{\"_id\":{\"$binary\":\"ugzpW1yjTEiRl8aNyhf9uw==\",\"$type\":\"03\"},\"AString\":\"Man!\",\"AnInt\":667,\"ADateTimeUTC\":{\"$date\":\"2019-02-12T13:27:38.680Z\"},\"ADateTimeLocal\":{\"$date\":\"2019-02-12T13:27:38.680Z\"},\"ADateTimeOffset\":[{\"$numberLong\":\"636855748586802930\"},0],\"AnArrayOfStrings\":[\"supreme\",\"executive\",\"power\",\"derives\",\"from\",\"a\",\"mandate\",\"from\",\"the\",\"masses\"]}\r\n{\"_id\":{\"$binary\":\"56pmbbItL0WCY4CA+y7eJQ==\",\"$type\":\"03\"},\"AString\":\"Denis\",\"AnInt\":204,\"ADateTimeUTC\":{\"$date\":\"2019-02-12T13:27:38.680Z\"},\"ADateTimeLocal\":{\"$date\":\"2019-02-12T13:27:38.680Z\"},\"ADateTimeOffset\":[{\"$numberLong\":\"636855748586802950\"},0],\"AnArrayOfStrings\":[\"not\",\"some\",\"farcical\",\"aquatic\",\"ceremony\"]}\r\n";

        static readonly string temp_file = Path.GetTempPath() + "import.json";
        static readonly string collection_name = typeof(ADocument).FullName;
        static IEnumerable<ADocument> docs;

        Establish context = () => File.WriteAllText(temp_file, import_file_contents);

        Because of = () =>
        {
            Import<ADocument>(temp_file, collection_name);
            var collection = database.GetCollection<ADocument>(collection_name);
            docs = collection.AsQueryable().ToList();
        };

        It should_have_exported_the_file = () => docs.Count().ShouldEqual(3);

        Cleanup the_file = () => File.Delete(temp_file);
    }
}