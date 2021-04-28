<?php
$servername = "localhost";
$username = "mananikoDB";
$password = "mananikoDB1@3";
$dbname = "commonhybrid";


// Create connection
$conn = mysqli_connect($servername, $username, $password, $dbname);

// Check connection
if (!$conn) {
  die("Connection failed: " . mysqli_connect_error());
}

$errormsg['Status'] = 0;
$errormsg['Id'] = "";
if(!isset($_POST["obj"]) || empty($_POST["obj"])){
  $sapi_type = php_sapi_name();
  if (substr($sapi_type, 0, 3) == 'cgi')
      header("Status: 404 Not Found 111");
  else
      header("HTTP/1.1 404 Not Found 111");
  die;
}

if(!isset($_POST["id"]) || empty($_POST["id"])){
  $sapi_type = php_sapi_name();
  if (substr($sapi_type, 0, 3) == 'cgi')
      header("Status: 404 Not Found 222");
  else
      header("HTTP/1.1 404 Not Found 222");
  die;
}

if(!isset($_POST["password"]) || empty($_POST["password"])){
  $sapi_type = php_sapi_name();
  if (substr($sapi_type, 0, 3) == 'cgi')
      header("Status: 404 Not Found 333");
  else
      header("HTTP/1.1 404 Not Found 333");
  die;
}

$password = $_POST["password"];

$row_id = $_POST["id"];

$sql = "SELECT * FROM groups WHERE id = '$row_id' AND password = '$password'";
$result = mysqli_query($conn, $sql);

if (mysqli_num_rows($result) == 0) {
    $msg = array();
    $msg['msg'] = "login Failed !!!";
    $msg['status'] = 0;
    echo json_encode($msg);
    die;
}

$obj = json_decode($_POST["obj"]);




$sql = "SELECT * FROM content WHERE group_id = '$row_id'";
$result = mysqli_query($conn, $sql);

if (mysqli_num_rows($result) == 0) {

  $data = json_encode($obj);

  if(mysqli_query($conn, "INSERT INTO content (group_id, objects, cubes, camera, comments) VALUES ('$row_id', '$data', '', '', '') ")){
    die;
  }
  else{
    $sapi_type = php_sapi_name();
    if (substr($sapi_type, 0, 3) == 'cgi')
        header("Status: 404 Not Found 4 444");
    else
        header("HTTP/1.1 404 Not Found 444");
    die;
  }
  
}
else{

  $resData = mysqli_fetch_assoc($result);

  if($resData['objects']){
    $data1 = json_decode($resData['objects']);
    
    for($i = 0; $i < count($obj); $i++){
      array_push($data1, $obj[$i]);
    }
    
    $data = json_encode($data1);
  }
  else{
    $data = json_encode($obj);
  }

  if(mysqli_query($conn, "UPDATE content SET objects = '$data' WHERE group_id = '$row_id'")){
    die;
  }
  else{
    $sapi_type = php_sapi_name();
    if (substr($sapi_type, 0, 3) == 'cgi')
        header("Status: 404 Not Found 555");
    else
        header("HTTP/1.1 404 Not Found 555");
    die;
  }
  
}




?>