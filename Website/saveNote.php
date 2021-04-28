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

if(!isset($_POST["id"]) || empty($_POST["id"])){
  $sapi_type = php_sapi_name();
  if (substr($sapi_type, 0, 3) == 'cgi')
      header("Status: 404 Not Found");
  else
      header("HTTP/1.1 404 Not Found");
  die;
}

if(!isset($_POST["name"]) || empty($_POST["name"])){
  $sapi_type = php_sapi_name();
  if (substr($sapi_type, 0, 3) == 'cgi')
      header("Status: 404 Not Found");
  else
      header("HTTP/1.1 404 Not Found");
  die;
}

if(!isset($_POST["desc"]) || empty($_POST["desc"])){
  $sapi_type = php_sapi_name();
  if (substr($sapi_type, 0, 3) == 'cgi')
      header("Status: 404 Not Found");
  else
      header("HTTP/1.1 404 Not Found");
  die;
}

if(!isset($_POST["password"]) || empty($_POST["password"])){
  $sapi_type = php_sapi_name();
  if (substr($sapi_type, 0, 3) == 'cgi')
      header("Status: 404 Not Found");
  else
      header("HTTP/1.1 404 Not Found");
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

$obj = new stdClass;
$obj->name = $_POST["name"];
$obj->desc = $_POST["desc"];



$sql = "SELECT * FROM content WHERE group_id = '$row_id'";
$result = mysqli_query($conn, $sql);

if (mysqli_num_rows($result) == 0) {

  $data = json_encode(array($obj));

  if(mysqli_query($conn, "INSERT INTO content (group_id, objects, cubes, camera, comments) VALUES ('$row_id', '', '', '', '$data') ")){
    die;
  }
  else{
    $sapi_type = php_sapi_name();
    if (substr($sapi_type, 0, 3) == 'cgi')
        header("Status: 404 Not Found");
    else
        header("HTTP/1.1 404 Not Found");
    die;
  }
  
}
else{

  $resData = mysqli_fetch_assoc($result);

  if($resData['comments']){
    $data1 = json_decode($resData['comments']);
    array_push($data1, $obj);
    $data = json_encode($data1);
  }
  else{
    $data = json_encode(array($obj));
  }

  if(mysqli_query($conn, "UPDATE content SET comments = '$data' WHERE group_id = '$row_id'")){
    die;
  }
  else{
    $sapi_type = php_sapi_name();
    if (substr($sapi_type, 0, 3) == 'cgi')
        header("Status: 404 Not Found");
    else
        header("HTTP/1.1 404 Not Found");
    die;
  }
  
}




?>