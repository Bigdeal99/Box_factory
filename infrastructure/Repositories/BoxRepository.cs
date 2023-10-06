using Dapper;
using infrastructure.DataModels;
using infrastructure.QueryModels;
using Npgsql;

namespace infrastructure.Repositories;

public class BoxRepository
{
    private NpgsqlDataSource _dataSource;

    public BoxRepository(NpgsqlDataSource datasource)
    {
        _dataSource = datasource;
    }

    public IEnumerable<BoxFeedQuery> GetBoxForFeed()
    {
        string sql = $@"
SELECT box_id as {nameof(BoxFeedQuery.BoxId)},
       box_name as {nameof(BoxFeedQuery.BoxName)},
       box_weight as {nameof(BoxFeedQuery.BoxWeight)},
FROM box_factory.boxes;
";
        using (var conn = _dataSource.OpenConnection())
        {
            return conn.Query<BoxFeedQuery>(sql);
        }
    }


    public Box UpdateBox(int boxId, string boxName, double boxWeight)
    {
        var sql = $@"
UPDATE box_factory.boxes SET box_name = @boxName,box_weight = @boxWeight
WHERE box_id = @boxId
RETURNING book_id as {nameof(Box.BoxId)},
       book_title as {nameof(Box.BoxName)},
       box_weight as {nameof(BoxFeedQuery.BoxWeight)},
";

        using (var conn = _dataSource.OpenConnection())
        {
            return conn.QueryFirst<Box>(sql, new { boxId, boxName, boxWeight });
        }
    }

    public Box CreateBox(string boxName, double boxWeight)
    {
        var sql = $@"
INSERT INTO box_factory.boxes (box_name, box_weight) 
VALUES (@boxName, @boxWeight)
RETURNING box_id as {nameof(Box.BoxId)},
       box_name as {nameof(Box.BoxName)},
       box_weight as {nameof(BoxFeedQuery.BoxWeight)},
        
";
        using (var conn = _dataSource.OpenConnection())
        {
            return conn.QueryFirst<Box>(sql, new { boxName, boxWeight });
        }
    }

    public bool DeleteBox(int boxId)
    {
        var sql = @"DELETE FROM box_factory.boxes WHERE box_Id = @boxId;";
        using (var conn = _dataSource.OpenConnection())
        {
            return conn.Execute(sql, new { boxId }) == 1;
        }
    }

    public bool DoesBoxtWithNameExist(string boxName)
    {
        var sql = @"SELECT COUNT(*) FROM box_factory.boxes WHERE box_name = @boxName;";
        using (var conn = _dataSource.OpenConnection())
        {
            return conn.ExecuteScalar<int>(sql, new { boxName }) == 1;
        }
    }
}