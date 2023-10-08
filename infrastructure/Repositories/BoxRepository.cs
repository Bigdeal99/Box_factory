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
SELECT boxid as {nameof(BoxFeedQuery.BoxId)},
       boxname as {nameof(BoxFeedQuery.BoxName)},
       boxweight as {nameof(BoxFeedQuery.BoxWeight)}
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
UPDATE box_factory.boxes SET boxname = @boxName,boxweight = @boxWeight
WHERE boxid = @boxId
RETURNING boxid as {nameof(Box.BoxId)},
       boxname as {nameof(Box.BoxName)},
       boxweight as {nameof(BoxFeedQuery.BoxWeight)}
";

        using (var conn = _dataSource.OpenConnection())
        {
            return conn.QueryFirst<Box>(sql, new { boxId, boxName, boxWeight });
        }
    }

    public Box CreateBox(string boxName, double boxWeight)
    {
        var sql = $@"
INSERT INTO box_factory.boxes (boxname, boxweight) 
VALUES (@boxName, @boxWeight)
RETURNING boxid as {nameof(Box.BoxId)},
       boxname as {nameof(Box.BoxName)},
       boxweight as {nameof(BoxFeedQuery.BoxWeight)}
        
";
        using (var conn = _dataSource.OpenConnection())
        {
            return conn.QueryFirst<Box>(sql, new { boxName, boxWeight });
        }
    }

    public bool DeleteBox(int boxId)
    {
        var sql = @"DELETE FROM box_factory.boxes WHERE boxId = @boxId;";
        using (var conn = _dataSource.OpenConnection())
        {
            return conn.Execute(sql, new { boxId }) == 1;
        }
    }

    public bool DoesBoxtWithNameExist(string boxName)
    {
        var sql = @"SELECT COUNT(*) FROM box_factory.boxes WHERE boxname = @boxName;";
        using (var conn = _dataSource.OpenConnection())
        {
            return conn.ExecuteScalar<int>(sql, new { boxName }) == 1;
        }
    }

    public async Task<Box> GetBoxByIdAsync(int boxId)
    {
        string sql = $@"
SELECT boxid as {nameof(Box.BoxId)},
       boxname as {nameof(Box.BoxName)},
       boxweight as {nameof(Box.BoxWeight)}
FROM box_factory.boxes
WHERE boxid = @boxId;
";

        using (var conn = _dataSource.OpenConnection())
        {
            return await conn.QueryFirstOrDefaultAsync<Box>(sql, new { boxId });
        }
    }
}