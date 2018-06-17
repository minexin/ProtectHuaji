var mysql = require('mysql')
var mysqlSetting = {
    host    : 'localhost',
    user    : 'root',
    password: 'password',
    database: 'huaji'
}

function getLeaderboard(callback){
    var sql = 'select * from score order by score desc limit 10'

    var connection = mysql.createConnection(mysqlSetting)
    connection.connect()
    connection.query(sql, function(error, rows){
        if (error) throw error
        callback(rows)
    })
    connection.end()
}

function insert(name, score, callback){
    var sql = 'insert into score values ("' + name +'",' + score + ')'

    var connection = mysql.createConnection(mysqlSetting)
    connection.connect()
    connection.query(sql, function(error, result){
        if (error) throw error
    })
    connection.end()
}

exports.getLeaderboard = getLeaderboard
exports.insert = insert