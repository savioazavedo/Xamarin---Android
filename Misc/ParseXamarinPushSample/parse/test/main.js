Parse.Cloud.afterSave( "Post", function(request) {

                 //Get value from Ticket Object
                 // var username = request.object.get("username");

                  //Set push query
                  var pushQuery = new Parse.Query(Parse.Installation);
                  //pushQuery.equalTo("username",username);

                  //Send Push message
                  Parse.Push.send({
                                  where: pushQuery,
                                  data: {
                                  alert: "New Ticket Added",
                                  sound: "default"
                                  }
                                  },{
                                  success: function(){
                                  response.success('true');
                                  },
                                  error: function (error) {
                                  response.error(error);
                                  }
                 });
});