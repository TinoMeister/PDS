import React from "react";
import { useNavigate } from 'react-router-dom';
import { Link } from "react-router-dom";
import SCLOGO from "../assets/robo.jpg";

import ImportCss from '../ImportCss';

const Signup = () => {
  const navigate = useNavigate();

  ImportCss('Auth');

  const create = async (e) => {
    e.preventDefault();
    navigate('/backOffice/');
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
                  placeholder="Francisco Manuel da Silva Arantes"
                  autoComplete="off"
                  required
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
                  placeholder="Francisco Arantes"
                  autoComplete="off"
                  required
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
                  placeholder="example@gmail.com"
                  autoComplete="off"
                  required
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
                  placeholder="Password"
                  autoComplete="off"
                  required
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
