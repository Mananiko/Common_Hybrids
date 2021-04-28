<div class="col-sm-6">

<script type="text/javascript">

function validateEmail(email) {
    const re = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(String(email).toLowerCase());
}

$(document).ready(function(){
    $('#check_button').click(function(){
        let group = $("#check_group").val();
        let email = $("#check_email").val();
        let password = $("#check_password").val();
        if(group.length == 0){
            alert("Group name is empty !!!");
            return false;
        }
        if(!validateEmail(email)){
            alert("Email is not correct !!!");
            return false;
        }
        if(password.length == 0){
            alert("Password name is empty !!!");
            return false;
        }


        $.ajax({
            type: "POST",
            url: "check.php",
            data: {group: group, email: email, password: password},
            dataType: 'json',
            success: function(data) {
                if(data.status){
                    $("#check_group").val('');
                    $("#check_email").val('');
                    $("#check_password").val('');
                }
                alert(data.msg);
            },

        });

    });
});
</script>

<div class="row text">
           <span class="textheaderregistration"><h2> Status Check </h2></span> 
           
           <div>This part of the panel is for checking whether a game is
available to enter or is played by another player at the
moment. Please fill out the form to check your current
status if you are not able to enter the game.</div>
</div>

    <form action="register.php" method="post">
    <input type="text" class="input_example" name="group" placeholder="group" id="check_group"/> <br/>
    <input type="text" class="input_example" name="email" placeholder="email" id="check_email"/><br/>
    <input type="text" class="input_example" name="password" placeholder="password" id="check_password"/><br/>
    <button class="input_submit" type="button" id="check_button">Check</button>
    </form>
</div>