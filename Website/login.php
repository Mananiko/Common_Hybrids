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
$errormsg['User_id'] = "";


if(!isset($_GET["group"]) || empty($_GET["group"])){
  echo json_encode($errormsg);
  die;
}

if(!isset($_GET["email"]) || empty($_GET["email"])){
  echo json_encode($errormsg);
  die;
}

if(!isset($_GET["password"]) || empty($_GET["password"])){
  echo json_encode($errormsg);
  die;
}


$group = $_GET["group"];
$emails =  $_GET["email"];
$password = $_GET["password"];


$sql = "SELECT b.id, b.isActive, a.id AS user_id FROM users AS a INNER JOIN groups AS b ON a.group_id = b.id 
       WHERE b.name = '$group' AND  b.password = '$password'  AND  a.email = '$emails'";
$result = mysqli_query($conn, $sql);

if (mysqli_num_rows($result) == 0) {
    $msg['Id'] = "";
    $msg['User_id'] = "";
    $msg['Status'] = 0;
    echo json_encode($msg);
    die;
}
else{
    $row = mysqli_fetch_assoc($result);
    $row_id = $row['id'];
    $row_user_id = $row['user_id'];

    if($row['isActive'] == 1){
      $msg['Id'] = '';
      $msg['User_id'] = '';
      $msg['Status'] = 2;
    }
    else{
      $msg['Id'] = (string) $row_id;
      $msg['User_id'] = (string) $row_user_id;
      $msg['Status'] = 1;

      mysqli_query($conn, "UPDATE groups SET isActive = 1 WHERE id = '$row_id'");

    }
    
    echo json_encode($msg);
    die;
}


?>