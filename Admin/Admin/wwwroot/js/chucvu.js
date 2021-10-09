$(document).ready(function()
{ $("#btn-add").click(function()
{

    $.ajaxSetup({ headers:
    { 'X-CSRF-TOKEN': $('meta[name="csrf-token"]').attr('content') } });
     $.ajax({ type:
         'POST', url:
          '/positions',
           data: { TenCV: $("#frmAddTask input[name=TenCV]").val() },
            dataType: 'json',
            success:
     function(chucvus)
     {
          $('#frmAddTask').trigger("reset"); $("#frmAddTask .close").click();


           var len = chucvus.length;
             var string="";

         for(let i = 0; i<len;i++)
         {
           string+="<tr><td>" +  chucvus[i].TenCV + "</td>";

           string+="<td>"+"  <a onclick='event.preventDefault();editChucVu("+chucvus[i].MaCV+");'"+" href='#' class='edit open-modal btn btn-warning' data-toggle='modal' data-target='#editTaskModal' value='"+chucvus[i].MaCV+"'><i class='fas fa-user-edit'></i></a>";
           string+=" <a onclick='event.preventDefault();deleteChucVu("+chucvus[i].MaCV+");'"+" href='#' class='delete open-modal btn btn-danger' data-toggle='modal' data-target='#deleteTaskModal' value='"+chucvus[i].MaCV+"'><i class='fas fa-trash'></i></a>";

           string+= "</tr>";

         }
         $(".modal-backdrop").remove();
           $("#body").html(string);


    }, error: function(data)

     {
        $(".modal-backdrop").remove();
       alert("Thêm thất bại") } }); });


     $("#btn-edit").click(function()
     { $.ajaxSetup({
          headers: { 'X-CSRF-TOKEN': $('meta[name="csrf-token"]').attr('content') } });
           $.ajax({
               type: 'PUT',
                url: '/positions/' + $("#frmEditTask input[name=MaCV]").val(),
                 data: { TenCV: $("#frmEditTask input[name=TenCV]").val(), },
                  dataType: 'json',
                   success: function(chucvus)
                   { $('#frmEditTask').trigger("reset"); $("#frmEditTask .close").click();

                   var len = chucvus.length;
                   var string="";

               for(let i = 0; i<len;i++)
               {
                 string+="<tr><td>" +  chucvus[i].TenCV + "</td>";

                 string+="<td>"+"  <a onclick='event.preventDefault();editChucVu("+chucvus[i].MaCV+");'"+" href='#' class='edit open-modal btn btn-warning' data-toggle='modal' data-target='#editTaskModal' value='"+chucvus[i].MaCV+"'><i class='fas fa-user-edit'></i></a>";
                 string+=" <a onclick='event.preventDefault();deleteChucVu("+chucvus[i].MaCV+");'"+" href='#' class='delete open-modal btn btn-danger' data-toggle='modal' data-target='#deleteTaskModal' value='"+chucvus[i].MaCV+"'><i class='fas fa-trash'></i></a>";

                 string+= "</tr>";

               }
               $(".modal-backdrop").remove();
                 $("#body").html(string); }, error: function(data) { var errors = $.parseJSON(data.responseText); $('#edit-task-errors').html(''); $.each(errors.messages, function(key, value) { $('#edit-task-errors').append(''+ value+''); }); $("#edit-error-bag").show(); } }); });
  $("#btn-delete").click(function()
   { $.ajaxSetup({
       headers: { 'X-CSRF-TOKEN': $('meta[name="csrf-token"]').attr('content') } });
        $.ajax({ type: 'DELETE',
         url: '/positions/' + $("#frmDeleteTask input[name=MaCV]").val(),
         dataType: 'json',
        success: function(chucvus) { $("#frmDeleteTask .close").click();
        var len = chucvus.length;
        var string="";

     for(let i = 0; i<len;i++)
     {
       string+="<tr><td>" +  chucvus[i].TenCV + "</td>";

       string+="<td>"+"  <a onclick='event.preventDefault();editChucVu("+chucvus[i].MaCV+");'"+" href='#' class='edit open-modal btn btn-warning' data-toggle='modal' data-target='#editTaskModal' value='"+chucvus[i].MaCV+"'><i class='fas fa-user-edit'></i></a>";
       string+=" <a onclick='event.preventDefault();deleteChucVu("+chucvus[i].MaCV+");'"+" href='#' class='delete open-modal btn btn-danger' data-toggle='modal' data-target='#deleteTaskModal' value='"+chucvus[i].MaCV+"'><i class='fas fa-trash'></i></a>";

       string+= "</tr>";
      }

      $(".modal-backdrop").remove();
      $("#body").html(string);


    }
      , error: function(data) { console.log(data); } });
    });
});


    function addChucVu()
    {
        $(document).ready(function() { $("#add-error-bag").hide();
        $('#addTaskModal').modal('show'); });

    }
    function editChucVu(MaCV)
    {
        $.ajax( {
            type: 'GET',
            url: '/positions/' + MaCV,
             success: function(data)
                {   $("#edit-error-bag").hide();
                    $("#frmEditTask input[name=MaCV]").val(data.chucvu.MaCV);
                    $("#frmEditTask input[name=TenCV]").val(data.chucvu.TenCV);
                    $('#editTaskModal').modal('show');
                },
             error: function(data)
                {
                    console.log(data);
                }
             });
    }

    function deleteChucVu(MaCV)
    {   $.ajax({
            type:'GET',
            url: '/positions/' + MaCV,
            success: function(data)
             { $("#frmDeleteTask input[name=MaCV]").val(data.chucvu.MaCV);
               $('#deleteTaskModal').modal('show');
             },
            error: function(data)
            { console.log(data);
            } });
     }


