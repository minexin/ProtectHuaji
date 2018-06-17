// import
let express = require('express')
let mysqlClient = require('./mySQL.js')
let bodyParser = require('body-parser')

// init
let app = express()
app.use(bodyParser.json({limit: '1mb'}))
app.use(bodyParser.urlencoded({ extended: true }))

// router
app.get('/huaji/leaderboard', function(req, res){
    console.log('get')
    mysqlClient.getLeaderboard(function(result){
        var data = {}
        data.records = result
        res.json(data)
    })
})

app.post('/huaji/upload', function(req, res){
    var record = req.body
    mysqlClient.insert(record.name, record.score, function(result){
        res.send('success')
    })
})

let server = app.listen(8080, function () {
    var post = server.address().port

    console.log('Listening at http://localhost:%s', post)
})
