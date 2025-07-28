
document.getElementById('registerForm').addEventListener('submit', async function (event) {
  event.preventDefault();

  const username = document.getElementById('username').value.trim();
  const password = document.getElementById('password').value;
  const confirmPassword = document.getElementById('confirmPassword').value;
  const messageDiv = document.getElementById('message');
  messageDiv.textContent = '';

  if (password !== confirmPassword) {
    messageDiv.style.color = 'red';
    messageDiv.textContent = 'As senhas não coincidem.';
    return;
  }

  try {
    const response = await fetch('https://localhost:7028/api/user', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify({ username, password })
    });

    if (response.ok) {
      messageDiv.style.color = 'green';
      messageDiv.textContent = 'Cadastro realizado com sucesso! Redirecionando...';
      setTimeout(() => {
        window.location.href = 'index.html';
      }, 2000);
    } else {
      const errorText = await response.text();
      messageDiv.style.color = 'red';
      messageDiv.textContent = errorText;
    }
  } catch (error) {
    messageDiv.style.color = 'red';
    messageDiv.textContent = 'Erro ao conectar com o servidor.';
    console.error('Erro:', error);
  }
});
