const Report = require('../models').Report;

module.exports = {
    index: (req, res) => {
        let reports = Report.findAll().then(reports=>{
            res.render("report/index", {"reports": reports})
        });
    },
    createGet: (req, res) => {
        res.render("report/create");
    },
    createPost: (req, res) => {
        let args = req.body.report;
        console.log(args);
        Report.create(args).then(()=>{
            res.redirect("/");
        })
    },
    detailsGet: (req, res) => {
        let id = req.params.id;

        Report.findById(id).then(report=>{
            res.render("report/edit",{"report":report});
        })
    },
    deleteGet: (req, res) => {
        let id = req.params.id;

        Report.findById(id).then(report=>{
            res.render("report/delete",{"report":report});
        })

    },
    deletePost: (req, res) => {
        let id = req.params.id;

        Report.findById(id).then(report=>{
            report.destroy().then(()=>{
                res.redirect("/")
            })
        })
    }
};