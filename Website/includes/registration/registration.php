
<script type="text/javascript">

function validateEmail(email) {
    const re = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(String(email).toLowerCase());
}

$(document).ready(function(){
    $('#registrationBTN').click(function(){
        let gr = $("#reg_group").val();
        let em = $("#reg_email").val();
        let ems = $("#reg_emails").val();
        if(gr.length == 0){
            alert("Group name is empty !!!");
            return false;
        }
        console.log(em);
        if(!validateEmail(em)){
            alert("Email is not correct !!!");
            return false;
        }
        if(ems.length > 0){
            let mpsspl = ems.split(',');
            for(let i = 0; i < mpsspl.length; i++){
                if(mpsspl[i].trim() !== "" && !validateEmail(mpsspl[i].trim())){
                    alert("Email "+ mpsspl[i].trim() +" is not correct !!!");
                    return false;
                }
            }
        }

        //var passhash = CryptoJS.MD5(gr+"|"+em+"|"+ems+"|"+Date.now()).toString();
        //$("#reg_password").val(passhash);
        

        $.ajax({
            type: "POST",
            url: "reg.php",
            data: {group: gr, email: em, emails: ems},
            dataType: 'json',
            success: function(data) {
                if(data.status){
                    $("#reg_password").val(data.password);
                    
                    $("#reg_group").val('');
                    $("#reg_email").val('');
                    $("#reg_emails").val('');
                }
                alert(data.msg);
            },

        });

    });
});
</script>

<div class="col-sm-6">

<div class="row text">
           <span class="textheaderregistration"><h2> Registration</h2></span> 
           <div>This part of the panel is for registration of your group.
Please fill out the form.</div>

</div>

    <form action="register.php" method="post">
    <input type="text" class="input_example" name="group" placeholder="group" id="reg_group"/> <br/>
    <input type="text" class="input_example" name="email" placeholder="email" id="reg_email"/><br/>
    <textarea  class="textarea" name="emails" placeholder="Other players emails" id="reg_emails"></textarea><br/>
    <input type="text" class="input_example" name="password" placeholder="password" readonly="true" id="reg_password"/><br/>
    <button class="input_submit" id="registrationBTN" type="button" >Save</button>
    </form>
</div>

