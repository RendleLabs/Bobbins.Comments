using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace Bobbins.Comments.Migrate.Migrations
{
    public partial class Initial
    {
        public override IReadOnlyList<MigrationOperation> UpOperations =>
            new List<MigrationOperation>(base.UpOperations.Concat(CreateProcOperations()));

        private static IEnumerable<MigrationOperation> CreateProcOperations()
        {
            yield return new SqlOperation
            {
                Sql = @"CREATE FUNCTION increment_reply_count(id INTEGER) RETURNS VOID
                          AS $$
                            UPDATE ""Comments""
                            SET ""ReplyCount"" = ""ReplyCount"" + 1
                            WHERE ""Id"" = id
                          $$
                          LANGUAGE SQL;"
            };
            
            yield return new SqlOperation
            {
                Sql = @"CREATE FUNCTION add_up_vote(id INTEGER) RETURNS VOID
                          AS $$
                            UPDATE ""Comments""
                            SET ""UpVoteCount"" = ""UpVoteCount"" + 1
                            WHERE ""Id"" = id
                          $$
                          LANGUAGE SQL;"
            };
            
            yield return new SqlOperation
            {
                Sql = @"CREATE FUNCTION add_down_vote(id INTEGER) RETURNS VOID
                          AS $$
                            UPDATE ""Comments""
                            SET ""DownVoteCount"" = ""DownVoteCount"" + 1
                            WHERE ""Id"" = id
                          $$
                          LANGUAGE SQL;"
            };
            
            yield return new SqlOperation
            {
                Sql = @"CREATE FUNCTION change_up_vote(id INTEGER) RETURNS VOID
                          AS $$
                            UPDATE ""Comments""
                            SET ""UpVoteCount"" = ""UpVoteCount"" + 1,
                                ""DownVoteCount"" = ""DownVoteCount"" - 1
                            WHERE ""Id"" = id
                          $$
                          LANGUAGE SQL;"
            };
            
            yield return new SqlOperation
            {
                Sql = @"CREATE FUNCTION change_down_vote(id INTEGER) RETURNS VOID
                          AS $$
                            UPDATE ""Comments""
                            SET ""UpVoteCount"" = ""UpVoteCount"" - 1,
                                ""DownVoteCount"" = ""DownVoteCount"" + 1
                            WHERE ""Id"" = id
                          $$
                          LANGUAGE SQL;"
            };
        }
    }
}