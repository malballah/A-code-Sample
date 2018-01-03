var bootGrid= {
    getHeaderWithNew: function (newTemplate) {
    return "<div id=\"{{ctx.id}}\" class=\"{{css.header}}\"><div class=\"row\"><div class='col-md-12 bootgridMessage'></div>" +
             "<div class=\"col-sm-12 actionBar\"><div style='margin-top:3px;display: inline-block;'><p  class=\"{{css.search}}\"></p></div><p class=\"{{css.actions}}\"></p>"
             + (newTemplate!=undefined?newTemplate:"<button class='command-new btn btn-info pull-left'><i class='fa fa-plus'></i>New</button>")
             + "</div></div></div>";
    },
    getEditDeleteCommand:function(id) {
        return "<button type=\"button\" class=\"btn btn-xs btn-default command-edit\" data-row-id=\"" + id + "\"><span class=\"fa fa-pencil\"></span></button> " +
                       "<button type=\"button\" class=\"btn btn-xs btn-default command-delete\" data-row-id=\"" + id + "\"><span class=\"fa fa-trash-o\"></span></button>";
    },
    getEditCommand: function (id) {
        return "<button type=\"button\" class=\"btn btn-xs btn-default command-edit\" data-row-id=\"" + id + "\"><span class=\"fa fa-pencil\"></span></button> " ;
    },
    getDeleteCommand: function (id) {
        return "<button type=\"button\" class=\"btn btn-xs btn-default command-delete\" data-row-id=\"" + id + "\"><span class=\"fa fa-trash-o\"></span></button>";
    }
}