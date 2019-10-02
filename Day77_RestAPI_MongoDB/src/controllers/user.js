var User = require("../models/user");

const crypto = require("crypto");
const jwt = require("jsonwebtoken");

exports.register = async (req, h) => {
    try
    {
        var user = await User.findOne({"username":req.payload.username});
        if (user)
        {
            console.log("user exists");
            throw new Error("username exists");
        }

        const encrypted = crypto.createHmac("sha1", "secret key")
                                .update(req.payload.password)
                                .digest("base64");
                                console.log(encrypted);

        let userData = 
        {
            username: req.payload.username,
            password: encrypted,
            isadmin:false
        }

        var user = await User.create(userData);
        const userCount = await User.count({});
        var isadmin = userCount == 1 ? true : false;
        if (isadmin)
        {
            console.log("admin");            
            await user.setAdmin();  // 아래와 같은기능
            // await User.findOneAndUpdate({"username":userData.username}, {$set: {"isadmin": true}});
        }
        return ({ok:true, message:"registered successfully", isadmin:isadmin});

    }
    catch(err)
    {
        console.error(err);
        return {ok:false, message:"register failed"};
    }
};    

exports.login = async (req, h) => {
    try
    {
        const username = req.payload.username;
        const password = req.payload.password;
        console.log(username, password);

        var user = await User.findOneByUsername(username);
        if (!user)
        {
            console.log("user does not exist");
            throw new Error("user does not exist");
        }
        let token;
        if(user.verify(password)) 
        {
            console.log("verification ok!");
            token = await jwt.sign(     // 토큰생성
                {
                    _id: user._id,
                    username: user.username,
                    admin: user.isadmin
                },
                "NeverShareYourSecret",
                {
                    expiresIn:"7d",     // 토큰만료 기한
                    issuer: 'hwang.com',    // 발급자
                    subject:"userinfo"
                }
            );

            console.log("token: " + token);
        }
        else
        {
            console.log("verification failed");
            throw new Error("verification failed");
        }

        return {ok:true, message:"logged in successfully", token};
    }
    catch(err)
    {
        console.error(err);
        return {ok:false, message:"login failed", err};
    }
}

exports.list = async (req, h) => {
    return User.find({}).exec().then((user)=> {
        return {users: user};
    }).catch(err => {
        return {err : err};
    });
}

exports.remove = async (req, h) => {
    return User.findOne({"username":req.params.username}).exec().then(user=> {
        if (!user)
            return {err: "user not found"};
        user.remove();
    }).then(data => {
        return {message: "user deleted successfully"};    
    }).catch(err => {
        return {err : err};
    });
}