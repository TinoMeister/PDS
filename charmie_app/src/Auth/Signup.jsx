import React, { useState } from "react";
import { useNavigate } from 'react-router-dom';
import { Link } from "react-router-dom";
import SCLOGO from "../assets/robo.jpg";

import ImportCss from '../ImportCss';
import { postData } from '../db/db';

const Signup = () => {
  const navigate = useNavigate();
  const [name, setName] = useState('');
  const [email, setEmail] = useState('');
  const [username, setUserName] = useState('');
  const [password, setPassword] = useState('');

  ImportCss('Auth');

  const create = async (e) => {
    e.preventDefault();

    let userData = {
      'username': username,
      'password': password,
      'email': email,
      'identity': {
        'name': name
      }
    };

    userData = await postData('Users/Client', userData);

    if (!userData) 
    {
      alert("Erro creating!!!");
      console.log(userData);
      return;
    }

    let token = await postData('Users/BearerToken/', {'username': username, 'password': password});

    if (!token) 
    {
      alert("Erro generating token!!!");
      console.log(token);
      return;
    }

    localStorage.setItem('user-token', token['token']);
    localStorage.setItem('user-info', JSON.stringify(userData));

    alert("Criado com sucesso");

    navigate('/backOffice');
    setName('');
    setEmail('');
    setUserName('');
    setPassword('');
  }

  return (
    <div className="auth-multi-layout">
      <div className="auth-box">
        <React.Fragment>
          <div className="auth-header">
            <div className="auth-header-logo">
              <img src={SCLOGO} alt="" className="auth-header-logo-img" />
            </div>
            <h1 className="auth-header-title">Create Account</h1>
            <p className="auth-header-subtitle">
              Create your account and be part of us
            </p>
          </div>
          <div className="auth-body">
            <form className="auth-form-validation">
              <div className="input-field">
                <label htmlFor="full-name" className="input-label">
                  Full Name
                </label>
                <input
                  type="text"
                  className="input-control"
                  id="full-name"
                  autoComplete="off"
                  required
                  onChange={(e) => setName(e.target.value)}
                  value={name}
                />
              </div>
              <div className="input-field">
                <label htmlFor="username" className="input-label">
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
                <label htmlFor="email" className="input-label">
                  Email address
                </label>
                <input
                  type="text"
                  className="input-control"
                  id="email"
                  autoComplete="off"
                  required
                  onChange={(e) => setEmail(e.target.value)}
                  value={email}
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
              <button onClick={create} type="submit" className="btn-submit">
                Create account
              </button>
            </form>
            <p className="text-center">
              Already have an account?{" "}
              <Link to={"/auth/signin"} className="link-text-center">
                Sign in instead
              </Link>
            </p>
          </div>
        </React.Fragment>
      </div>
    </div>
  );
};

export default Signup;
