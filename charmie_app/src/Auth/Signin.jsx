import React, { useState } from "react";
import { useNavigate } from 'react-router-dom';
import { Link } from "react-router-dom";
import SCLOGO from "../assets/robo.jpg";

import ImportCss from '../ImportCss';
import { getData, postData } from '../db/db';

const Signin = () => {
  const navigate = useNavigate();
  const [username, setUserName] = useState('');
  const [password, setPassword] = useState('');
  
  ImportCss('Auth');

  const login = async (e) => {
    e.preventDefault();

    let userData = {
      'username': username,
      'password': password
    };

    let token = await postData('Users/BearerToken/', userData);

    if (!token) 
    {
      alert("User / Password erradas!!!");
      return;
    }

    let user = await getData(`Users/${username}`, token['token']);

    if (!user) 
    {
      alert("Erro generating token!!!");
      return;
    }

    localStorage.setItem('user-token', token['token']);
    localStorage.setItem('user-info', JSON.stringify(user));

    alert("Login com sucesso");

    navigate('/backOffice');
    setUserName('');
    setPassword('');
  }

  return (
    <>
      <div className="auth-multi-layout">
        <div className="auth-box">
          <React.Fragment>
            <div className="auth-header">
              <div className="auth-header-logo">
                <img src={SCLOGO} alt="" className="auth-header-logo-img" />
              </div>
              <h1 className="auth-header-title"></h1>
              <p className="auth-header-subtitle">
                Sign-in to your account and start the adventure
              </p>
            </div>
            <div className="auth-body">
              <form className="auth-form-validation">
                <div className="input-field">
                  <label type="text" className="input-label">
                    Username
                  </label>
                  <input
                    type="text"
                    className="input-control"
                    id="username"
                    autoComplete="off"
                    required
                    onChange={(e) => setUserName(e.target.value)}
                    value={username}
                  />
                </div>
                <div className="input-field">
                  <label htmlFor="password" className="input-label">
                    Password
                  </label>
                  <input
                    type="password"
                    name="password"
                    id="password"
                    className="input-control"
                    autoComplete="off"
                    required
                    onChange={(e) => setPassword(e.target.value)}
                    value={password}
                  />
                </div>
                <button onClick={login} type="submit" className="btn-submit">
                  Sign in
                </button>
              </form>
              <p className="text-center">
                New on our platform?{" "}
                <Link to={"/auth/signup"} className="link-text-center">
                  Create account here
                </Link>
              </p>
            </div>
          </React.Fragment>
        </div>
      </div>
    </>
  );
};

export default Signin;
