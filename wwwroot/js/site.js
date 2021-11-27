// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const showLogoutWarning = () => {
  document.querySelector("#logout-warning").classList.remove("hidden");
};

const logoutUser = () => {
  window.location.href = "/Logout";
};

const main = () => {
  setTimeout(() => {
    showLogoutWarning();
  }, 1080000); // 18 minutes

  setTimeout(() => logoutUser(), 1200000); // 20 minutes
};

main();
