// import { useState } from "react";
// import "../home/home.css";
// import { useNavigate } from "react-router";
// import BGImage from "../../assets/BG.jpeg";
// import axios from "axios";
// import { APP_BACKEND_URL } from "../../constants/app-url";

// const LogIn = () => {
//   const navigate = useNavigate();

//   const [showPassword, setShowPassword] = useState(false);
//   const [username, setUsername] = useState("");
//   const [password, setPassword] = useState("");

//   const handleLogin = async () => {
//     const response = await axios.post(`${APP_BACKEND_URL}/Auth/login`, {
//       email: username,
//       password: password,
//     });
//     const { user, token } = response.data;

//     if (user) {
//       alert("Welcome " + user.fullName);

//       localStorage.setItem(
//         "user",
//         JSON.stringify({
//           username: user.fullName,
//           email: user.email,
//           role: user.role,
//           id: user.userId,
//           token,
//         })
//       );

//       navigate("/dashboard");
//     } else {
//       alert("Username or password is incorrect");
//     }
//   };

//   const backButton = () => {
//     navigate("/");
//   };

//   const togglePassword = () => {
//     setShowPassword((prev) => !prev);
//   };

//   return (
//     <div className="login-bg" style={{ backgroundImage: `url(${BGImage})` }}>
//       <div className="login-wrapper">
//         <div className="login-card">
//           <h1 className="login-title">Welcome Back</h1>
//           <p className="login-subtitle">Log in to continue using Booker!</p>

//           <div className="input-group">
//             <label className="input-label">Username</label>
//             <input
//               type="text"
//               className="user-input"
//               placeholder="Enter your username"
//               value={username}
//               onChange={(e) => setUsername(e.target.value)}
//             />
//           </div>

//           <div className="input-group">
//             <label className="input-label">Password</label>
//             <div className="password-wrapper">
//               <input
//                 type={showPassword ? "text" : "password"}
//                 className="user-input password-input"
//                 placeholder="Enter your password"
//                 value={password}
//                 onChange={(e) => setPassword(e.target.value)}
//               />
//               <button
//                 type="button"
//                 className="view-pass-btn"
//                 onClick={togglePassword}
//                 style={{ backgroundColor: "rgb(255, 136, 0)" }}
//               >
//                 {showPassword ? "Hide" : "Show"}
//               </button>
//             </div>
//           </div>

//           <button
//             className="primary-btn"
//             style={{ backgroundColor: "rgb(255, 136, 0)" }}
//             onClick={handleLogin}
//           >
//             Log In
//           </button>

//           <button className="secondary-btn" onClick={backButton}>
//             Back
//           </button>
//         </div>
//       </div>
//     </div>
//   );
// };

// export default LogIn;




import { useState } from "react";
import "../home/home.css";
import { useNavigate } from "react-router";
import BGImage from "../../assets/BG.jpeg";
import axios from "axios";
import { APP_BACKEND_URL } from "../../constants/app-url";

const LogIn = () => {
  const navigate = useNavigate();

  const [showPassword, setShowPassword] = useState(false);
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [loading, setLoading] = useState(false);
  const [popup, setPopup] = useState({ show: false, type: "", message: "" });

  const showPopup = (type, message) => {
    setPopup({ show: true, type, message });
    setTimeout(() => {
      setPopup({ show: false, type: "", message: "" });
    }, 4000);
  };

  const handleLogin = async () => {
    // Validation
    if (!username.trim() || !password.trim()) {
      showPopup("error", "Please enter both username and password");
      return;
    }

    setLoading(true);

    try {
      const response = await axios.post(`${APP_BACKEND_URL}/Auth/login`, {
        email: username,
        password: password,
      });

      const { user, token } = response.data;

      if (user && token) {
        showPopup("success", `Welcome back, ${user.fullName}!`);

        localStorage.setItem(
          "user",
          JSON.stringify({
            username: user.fullName,
            email: user.email,
            role: user.role,
            id: user.userId,
            token,
          })
        );

        // Navigate after a short delay to show success message
        setTimeout(() => {
          navigate("/dashboard");
        }, 1500);
      } else {
        showPopup("error", "Invalid response from server");
      }
    } catch (error) {
      // Handle different error scenarios
      if (error.response) {
        // Server responded with error status
        const status = error.response.status;
        const message = error.response.data?.message;

        if (status === 401 || status === 403) {
          showPopup("error", "Incorrect username or password");
        } else if (status === 404) {
          showPopup("error", "User not found");
        } else if (status === 500) {
          showPopup("error", "Server error. Please try again later");
        } else {
          showPopup("error", message || "Login failed. Please try again");
        }
      } else if (error.request) {
        // Request made but no response
        showPopup("error", "No response from server. Check your connection");
      } else {
        // Something else went wrong
        showPopup("error", "An unexpected error occurred");
      }
      console.error("Login error:", error);
    } finally {
      setLoading(false);
    }
  };

  const backButton = () => {
    navigate("/");
  };

  const togglePassword = () => {
    setShowPassword((prev) => !prev);
  };

  const handleKeyPress = (e) => {
    if (e.key === "Enter" && !loading) {
      handleLogin();
    }
  };

  return (
    <div className="login-bg" style={{ backgroundImage: `url(${BGImage})` }}>
      {/* Popup Notification */}
      {popup.show && (
        <div
          style={{
            position: "fixed",
            top: "20px",
            right: "20px",
            zIndex: 1000,
            padding: "16px 24px",
            borderRadius: "8px",
            backgroundColor: popup.type === "success" ? "#10b981" : "#ef4444",
            color: "white",
            boxShadow: "0 4px 12px rgba(0,0,0,0.15)",
            animation: "slideIn 0.3s ease-out",
            maxWidth: "400px",
          }}
        >
          <div style={{ display: "flex", alignItems: "center", gap: "12px" }}>
            <span style={{ fontSize: "20px" }}>
              {popup.type === "success" ? "✓" : "⚠"}
            </span>
            <span style={{ fontWeight: "500" }}>{popup.message}</span>
          </div>
        </div>
      )}

      <div className="login-wrapper">
        <div className="login-card">
          <h1 className="login-title">Welcome Back</h1>
          <p className="login-subtitle">Log in to continue using Booker!</p>

          <div className="input-group">
            <label className="input-label">Email</label>
            <input
              type="text"
              className="user-input"
              placeholder="Enter your username"
              value={username}
              onChange={(e) => setUsername(e.target.value)}
              onKeyPress={handleKeyPress}
              disabled={loading}
            />
          </div>

          <div className="input-group">
            <label className="input-label">Password</label>
            <div className="password-wrapper">
              <input
                type={showPassword ? "text" : "password"}
                className="user-input password-input"
                placeholder="Enter your password"
                value={password}
                onChange={(e) => setPassword(e.target.value)}
                onKeyPress={handleKeyPress}
                disabled={loading}
              />
              <button
                type="button"
                className="view-pass-btn"
                onClick={togglePassword}
                style={{ backgroundColor: "rgb(255, 136, 0)" }}
                disabled={loading}
              >
                {showPassword ? "Hide" : "Show"}
              </button>
            </div>
          </div>

          <button
            className="primary-btn"
            style={{
              backgroundColor: loading ? "#ccc" : "rgb(255, 136, 0)",
              cursor: loading ? "not-allowed" : "pointer",
            }}
            onClick={handleLogin}
            disabled={loading}
          >
            {loading ? "Logging in..." : "Log In"}
          </button>

          <button
            className="secondary-btn"
            onClick={backButton}
            disabled={loading}
          >
            Back
          </button>
        </div>
      </div>

      <style>{`
        @keyframes slideIn {
          from {
            transform: translateX(100%);
            opacity: 0;
          }
          to {
            transform: translateX(0);
            opacity: 1;
          }
        }
      `}</style>
    </div>
  );
};

export default LogIn;
