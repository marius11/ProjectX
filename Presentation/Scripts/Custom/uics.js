$(document).ready(function () {
  $("#btn-load-items").on("click", function (event) {
    event.preventDefault();

    $.ajax({
      url: "https://localhost:44355/api/items",
      type: "GET",
      dataType: "json",
      contentType: "application/json; charset=utf-8",

      beforeSend: function (response) {
        $("#btn-load-items").prop("disabled", true).css("cursor", "default");
        $("#btn-load-items i").addClass("fa fa-spinner fa-spin fa-fw");
      },

      success: function (response) {
        $("#table-items tbody").empty();
        $.each(response, function (key, value) {
          var tr = "<tr>";
          tr += "<td>" + value.Id + "</td>";
          tr += "<td>" + value.Text + "</td>";
          tr += "</tr>";
          $("#table-items tbody").append(tr);
        });
        $("#table-items").show();
        $("#hide-add-button").show();
        $("#btn-load-items").prop("disabled", true).css("cursor", "default");
      },
      error: function (response) {
        $("#btn-load-items").prop("disabled", false).css("cursor", "pointer");

        $("#message #error-message").fadeIn(250);
        setTimeout(function () {
          $("#message #error-message").fadeOut(500);
        }, 5000);
      },
      complete: function (response) {
        $("#btn-load-items i").removeClass("fa fa-spinner fa-spin fa-fw");
      }
    });
  });

  $("#btn-add-item").on("click", function (event) {
    event.preventDefault();

    var jsonData = { Text: $("textarea").val() };
    $.ajax({
      url: "https://localhost:44355/api/items",
      type: "POST",
      data: JSON.stringify(jsonData),
      dataType: "json",
      contentType: "application/json; charset=utf-8",

      beforeSend: function (response) {
        $("#btn-add-item").prop("disabled", true).css("cursor", "default");
        $("#btn-cancel-form").prop("disabled", true).css("cursor", "default");
        $("textarea").prop("disabled", true).css("cursor", "default");

        $("#btn-add-item i").addClass("fa fa-spinner fa-spin fa-fw");
      },

      success: function (response) {
        $("#table-items tbody").append("<tr><td>" +
          response["Id"] + "</td><td>" +
          response["Text"] + "</td></tr>");
        /*$("<tr><td>" + response["id"] + "</td><td>" +
            response["text"] + "</td></tr>").prependTo("#table-items > tbody");*/

        $("#btn-add-item").prop("disabled", false);
        $("#btn-cancel-form").prop("disabled", false);
        $("textarea").prop("disabled", false);

        $("#btn-show-form").show();
        $("#add-task-form").hide();
        $("textarea").val("");

        $("#btn-show-form").find(".glyphicon").removeClass("glyphicon-plus").addClass("glyphicon-ok");
        setTimeout(function () {
          $("#btn-show-form").find(".glyphicon").removeClass("glyphicon-ok").addClass("glyphicon-plus");
        }, 5000);

        $("#message #success-message").fadeIn(500);
        setTimeout(function () {
          $("#message #success-message").fadeOut(500);
        }, 5000);
      },

      error: function (response) {
        $("#btn-add-item").prop("disabled", false);

        $("#add-task-form").hide();
        $("#btn-show-form").show();

        $("textarea").val("");

        $("#btn-show-form").find(".glyphicon").removeClass("glyphicon-plus").addClass("glyphicon-remove");
        $("#btn-show-form").css("background-color", "#FF0000");

        setTimeout(function () {
          $("#btn-show-form").find(".glyphicon").removeClass("glyphicon-remove").addClass("glyphicon-plus");
          $("#btn-show-form").css("background-color", "#228B22");
        }, 5000);

        $("#message #error-message").fadeIn(500);
        setTimeout(function () {
          $("#message #error-message").fadeOut(500);
        }, 5000);
      },

      complete: function (response) {
        $("#btn-add-item").prop("disabled", false);
        $("#btn-cancel-form").prop("disabled", false).css("cursor", "pointer");
        $("textarea").prop("disabled", false).css("cursor", "text");

        $("#btn-add-item i").removeClass("fa fa-spinner fa-spin fa-fw");
      }
    });
  });

  // show form
  $("#btn-show-form").on("click", function () {
    $("#add-task-form").show();
    $("#btn-show-form").hide();
    $("#btn-add-item").prop("disabled", true).css("cursor", "default");
  });

  // hide form
  $("#btn-cancel-form").on("click", function () {
    $("#add-task-form").hide();
    $("#btn-show-form").show();
    $("textarea").val("");
  });

  // disabled/enable "Submit"
  $("textarea").on("input change", function () {
    if (this.value === "") {
      $("#btn-add-item").prop("disabled", true).css("cursor", "default");
    }
    else {
      $("#btn-add-item").prop("disabled", false).css("cursor", "pointer");
    }
  });
});