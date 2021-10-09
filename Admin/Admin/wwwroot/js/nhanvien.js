$(document).ready(function()
{
  $("#btn-delete").click(function()
   { $.ajaxSetup({ headers: { 'X-CSRF-TOKEN': $('meta[name="csrf-token"]').attr('content') } });
   $.ajax({ type: 'DELETE',
    url: '/employees/' + $("#frmDeleteNhanVien input[name=MaNV]").val(),
    dataType: 'json', success: function(nhanviens)
     { $("#frmDeleteNhanVien .close").click();
     var len = nhanviens.length;
     var string="";

  for(let i = 0; i<len;i++)
  {
    string+="<tr><td>" +  nhanviens[i].TenCV + "</td>";

    string+="<td>"+"  <a href='{{route(employees.show'," + nhanviens[i].MaNV + ")}}' class='btn btn-warning'><i class='fas fa-eye'></i></a>";
    string+=" <a onclick='event.preventDefault();deleteChucVu("+nhanviens[i].MaCV+");'"+" href='#' class='delete open-modal btn btn-danger' data-toggle='modal' data-target='#deleteTaskModal' value='"+nhanviens[i].MaCV+"'><i class='fas fa-trash'></i></a>";

    string+= "</tr>";

//     <a class="btn btn-warning" href="{{route('employees.show',$nhanvien->MaNV)}}"> <i class="fas fa-eye"></i>
//     </a>

// <a class="btn btn-success" href="{{route('employees.edit',$nhanvien->MaNV)}}"> <i class="fas fa-user-edit"></i> </a>
   }

   $(".modal-backdrop").remove();
   $("#body").html(string);

            }, error: function(data) { console.log(data); } }); }); });

    function deleteNhanVien(MaNV)
    { $.ajax({ type: 'POST',
    url: '/employees/' + MaNV, success: function(data)
     { $("#frmDeleteNhanVien input[name=MaNV]").val(data.MaNV);
     $('#deleteNhanVien').modal('show'); }, error: function(data) { console.log(data); } }); }
