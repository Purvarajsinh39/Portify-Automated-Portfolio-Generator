document.addEventListener('DOMContentLoaded', function () {
    const fab = document.getElementById('chatbot-fab');
    const windowEl = document.getElementById('chatbot-window');
    const closeBtn = document.getElementById('cb-close');
    const input = document.getElementById('chatbot-input');
    const sendBtn = document.getElementById('chatbot-send');
    const messagesContainer = document.getElementById('chatbot-messages');

    // Reusable HTML for the AI Avatar (Gemini Icon)
    const aiAvatarHTML = `
        <div class="cb-avatar">
            <div class="gemini-wrapper gemini-idle" style="--g-size: 18px;">
                <div class="gemini-layer gemini-glow"><div class="gemini-gradient gemini-gradient-anim"></div></div>
                <div class="gemini-layer gemini-primary"><div class="gemini-gradient gemini-gradient-anim"></div></div>
            </div>
        </div>
    `;

    // Reusable HTML for the User Avatar
    const userAvatarHTML = `<div class="cb-avatar"><i class="fas fa-user"></i></div>`;

    // Reusable HTML for the "Thinking" state (Bouncing Dots)
    const typingIndicatorHTML = `
        <div class="typing-indicator">
            <div class="dot"></div>
            <div class="dot"></div>
            <div class="dot"></div>
        </div>
    `;

    // Toggle Chat Window
    fab.addEventListener('click', () => {
        windowEl.classList.toggle('active');
        if (windowEl.classList.contains('active')) {
            input.focus();
        }
    });

    closeBtn.addEventListener('click', () => {
        windowEl.classList.remove('active');
    });

    // Send Message
    function sendMessage() {
        const text = input.value.trim();
        if (!text) return;

        // Add user message
        addMessage(text, 'user');
        input.value = '';

        // Show typing indicator
        const typingId = showTyping();

        // AJAX Call to Backend
        fetch('/Chatbot/Ask', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded',
            },
            body: `message=${encodeURIComponent(text)}`
        })
        .then(response => response.json())
        .then(data => {
            removeTyping(typingId);
            if (data.success) {
                addMessage(data.response, 'ai');
            } else {
                addMessage(data.response || 'Something went wrong.', 'ai');
                if (data.debug) {
                    console.error('Chatbot Debug Info:', data.debug);
                }
            }
        })
        .catch(error => {
            removeTyping(typingId);
            addMessage('Error: Could not connect to the server.', 'ai');
        });
    }

    sendBtn.addEventListener('click', sendMessage);
    input.addEventListener('keypress', (e) => {
        if (e.key === 'Enter') sendMessage();
    });

    function addMessage(text, sender) {
        const msgGroup = document.createElement('div');
        msgGroup.className = `cb-msg-group ${sender}`;
        
        const avatarWrapper = document.createElement('div');
        avatarWrapper.innerHTML = sender === 'ai' ? aiAvatarHTML : userAvatarHTML;
        
        const msgDiv = document.createElement('div');
        msgDiv.className = `message ${sender}`;
        
        // Simple markdown-ish formatting for line breaks and bold
        let formattedText = text
            .replace(/\n/g, '<br>')
            .replace(/\*\*(.*?)\*\*/g, '<strong>$1</strong>');
        
        msgDiv.innerHTML = formattedText;
        
        msgGroup.appendChild(avatarWrapper.firstElementChild);
        msgGroup.appendChild(msgDiv);
        messagesContainer.appendChild(msgGroup);
        
        // Scroll to show the start of the new message
        scrollToBottom(msgGroup);
    }

    function showTyping() {
        const id = 'typing-' + Date.now();
        const typingGroup = document.createElement('div');
        typingGroup.id = id;
        typingGroup.className = 'cb-msg-group ai';
        
        // Add AI avatar to the typing indicator group for better visibility
        const avatarWrapper = document.createElement('div');
        avatarWrapper.innerHTML = aiAvatarHTML;
        typingGroup.appendChild(avatarWrapper.firstElementChild);
        
        // Add the actual thinking animation
        const indicatorWrapper = document.createElement('div');
        indicatorWrapper.innerHTML = typingIndicatorHTML;
        typingGroup.appendChild(indicatorWrapper.firstElementChild);
        
        messagesContainer.appendChild(typingGroup);
        scrollToBottom(typingGroup);
        return id;
    }

    function removeTyping(id) {
        const el = document.getElementById(id);
        if (el) el.remove();
    }

    function scrollToBottom(el) {
        // Use a small timeout to ensure DOM has updated
        setTimeout(() => {
            if (el) {
                el.scrollIntoView({ behavior: 'smooth', block: 'start' });
            } else {
                messagesContainer.scrollTop = messagesContainer.scrollHeight;
            }
        }, 50);
    }

    // Initial greeting
    setTimeout(() => {
        if (messagesContainer.children.length === 0) {
            addMessage("Hello! I am Portify AI. How can I assist you today?", 'ai');
        }
    }, 1000);
});
