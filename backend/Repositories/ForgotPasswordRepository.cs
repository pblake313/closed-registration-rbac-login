using System;
using SAConstruction.Models;

namespace SAConstruction.Repositories
{
    public class ForgotPasswordRepository
    {
        private readonly DataContextDapper _dapper;

        public ForgotPasswordRepository(IConfiguration config)
        {
            _dapper = new DataContextDapper(config);
        }

        public PasswordResetDoc SetPasswordResetDoc(int userId)
        {
            var referenceId = Guid.NewGuid().ToString("N");
            var expiresAt = DateTime.UtcNow.AddMinutes(15);

            const string sql = @"
                -- Kill any previous reset docs for this user
                DELETE FROM Users.PasswordResetDocs
                WHERE UserId = @UserId;

                -- Insert new reset doc
                INSERT INTO Users.PasswordResetDocs (UserId, ExpiresAt, ReferenceId)
                VALUES (@UserId, @ExpiresAt, @ReferenceId);

                -- Return the freshly inserted row
                SELECT 
                    UserId,
                    DateCreated,
                    ExpiresAt,
                    ReferenceId
                FROM Users.PasswordResetDocs
                WHERE ReferenceId = @ReferenceId;
            ";

            var parameters = new
            {
                UserId = userId,
                ExpiresAt = expiresAt,
                ReferenceId = referenceId
            };

            var resetDoc = _dapper.LoadDataSingle<PasswordResetDoc>(sql, parameters);

            return resetDoc;
        }

        // 🔥 Get a *valid* (non-expired) reset doc by reference id
        public PasswordResetDoc GetPasswordResetDocByRefId(string refId)
        {
            const string sql = @"
                SELECT TOP 1
                    UserId,
                    DateCreated,
                    ExpiresAt,
                    ReferenceId
                FROM Users.PasswordResetDocs
                WHERE ReferenceId = @ReferenceId;
            ";

            var parameters = new { ReferenceId = refId };

            var docs = _dapper.LoadData<PasswordResetDoc>(sql, parameters);
            var doc = docs.FirstOrDefault();

            if (doc == null)
            {
                throw new Exception("Reset Doc Non Existant");
            }

            if (doc.ExpiresAt < DateTime.UtcNow)
            {
                // delete expired doc.

                DeleteResetDocByRefId(refId);

                throw new Exception("Reset Doc Expired");
            }

            return doc;
        }
        public void DeleteResetDocByRefId(string refId)
        {
            const string sql = @"
                DELETE FROM Users.PasswordResetDocs
                WHERE ReferenceId = @ReferenceId;
            ";

            bool deleted = _dapper.ExecuteSql(sql, new { ReferenceId = refId });

            if (!deleted)
            {
                throw new Exception("Reset doc not found — nothing deleted.");
            }

        }

    }
}
